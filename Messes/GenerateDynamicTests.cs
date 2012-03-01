// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace DynamicMSTest.Tasks.Remote
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using DynamicMSTest.Framework;
    using DynamicMSTest.Utilities;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    class GenerateDynamicTests : MarshalByRefObject
    {
        // These properties are explicitly set by the user when invoking the
        // build task (in the .csproj or the .targets).
        public string BasePath { get; set; }
        public string Context { get; set; }
        public List<TaskItem> GeneratedFiles { get; private set; }
        public ITaskItem Project { get; set; }
        public string DefiningAssembly { get; set; }

        RemoteLogger logger;

        public GenerateDynamicTests()
        {
            GeneratedFiles = new List<TaskItem>();
            logger = new RemoteLogger();
        }

        TestMethod ApplyDefaults(TestMethod method, MethodInfo info)
        {
            if (method.Class == null)
            {
                method.Class = info.DeclaringType.Name;
            }
            if (method.Namespace == null)
            {
                method.Namespace = info.DeclaringType.Namespace;
            }
            if (string.IsNullOrEmpty(method.Namespace))
            {
                method.Namespace = "DynamicMSTest.Default";
            }
            if (method.File == null)
            {
                method.File = method.ClassFullName.Replace(".", "\\") + ".cs";
            }
            string projectDirectory = Path.GetDirectoryName(Project.GetMetadata("FullPath"));
            if (File.Exists(Path.Combine(projectDirectory, method.File)))
            {
                string template =
                    Path.GetDirectoryName(method.File) +
                    Path.DirectorySeparatorChar + 
                    Path.GetFileNameWithoutExtension(method.File) +
                    ".g{0}";
                if (Path.HasExtension(method.File))
                {
                    template += Path.GetExtension(method.File);
                }
                int i = 0;
                string newFile = string.Format(CultureInfo.InvariantCulture, template, "");
                while (File.Exists(Path.Combine(projectDirectory, newFile)))
                {
                    newFile = string.Format(CultureInfo.InvariantCulture, template, i++);
                } 
                method.File = newFile;
            }
            return method;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Catch exceptions before they cross AppDomain boundary")]
        [SuppressMessage("Microsoft.Reliability", "CA2001:AvoidCallingProblematicMethods", Justification = "Nothing else loads into this AppDomain")]
        public RemoteExecutionResult Execute()
        {
            logger.LogMessage("Dynamic test framework: start remote task.");
            bool result = true;
            try
            {
                Assembly targetAssembly = Assembly.LoadFrom(DefiningAssembly);
                var clrMethods =
                    from type in targetAssembly.GetExportedTypes()
                    from method in type.GetMethods()
                    where method.GetCustomAttributes(typeof(DynamicTestMethodAttribute), false).Length != 0
                    select method;
                var testMethods =
                    from method in clrMethods
                    from testMethod in GenerateTestMethods(method)
                    select testMethod;

                var testClassesInRootNamespace =
                    from testMethod in testMethods
                    where string.IsNullOrEmpty(testMethod.Namespace)
                    group testMethod by testMethod.Class into classGrouping
                    select classGrouping.Key;

                foreach (string testClass in testClassesInRootNamespace)
                {
                    logger.LogWarning("Test method {0} was in the root namespace; moved to DynamicMSTest.Default.", testClass);
                }

                var testMethodsByFile =
                    from testMethod in testMethods
                    group testMethod by testMethod.File;

                if (testMethodsByFile.Count() == 0)
                    throw new NoTestsGeneratedException(targetAssembly);

                HashSet<string> clrTestClasses =
                    new HashSet<string>(from method in clrMethods
                                        group method by method.DeclaringType into typeGrouping
                                        let type = typeGrouping.Key
                                        where type.GetCustomAttributes(typeof(TestClassAttribute), false).Length != 0
                                        select type.FullName);

                IEnumerable<GeneratedFileInfo> fileInfos = PrepareFilesForGeneration(testMethodsByFile);
                List<TaskItem> newGeneratedFiles = FileGenerator.GenerateFilesForTestMethods(
                    fileInfos, BasePath, clrTestClasses);
                GeneratedFiles.AddRange(newGeneratedFiles);
            }
            catch (Exception e)
            {
                logger.LogException(e);
                logger.LogMessage("Dynamic test framework: end remote task with exception.");
                result = false;
            }

            logger.LogMessage("Dynamic test framework: end remote task.");
            return new RemoteExecutionResult { Success = result, Log = logger.Log };
        }

        IEnumerable<GeneratedFileInfo> PrepareFilesForGeneration(IEnumerable<IGrouping<string, TestMethod>> methodsByFile)
        {
            return
                from singleFile in methodsByFile
                select new GeneratedFileInfo { Path = singleFile.Key, Namespaces = PrepareNamespacesForGeneration(singleFile) };
        }

        IEnumerable<NamespaceInfo> PrepareNamespacesForGeneration(IEnumerable<TestMethod> methods)
        {
            var methodsByNamespace =
                from method in methods
                group method by method.Namespace;

            return
                from singleNamespace in methodsByNamespace
                let groupbyImports = singleNamespace.GroupBy(method => method.Imports, HashSet<string>.CreateSetComparer())
                from singleImportSet in groupbyImports
                select new NamespaceInfo { Name = singleNamespace.Key, Imports = singleImportSet.Key, Classes = PrepareClassesForGeneration(singleImportSet) };
        }

        IEnumerable<ClassInfo> PrepareClassesForGeneration(IEnumerable<TestMethod> methods)
        {
            var methodsByClass =
                from method in methods
                group method by new { Name = method.Class, FullName = method.ClassFullName, BaseClass = method.BaseClass, ArgumentsToBaseConstructor = method.ArgumentsToBaseConstructor };
            return from singleClass in methodsByClass
                   select new ClassInfo { Name = singleClass.Key.Name, FullName = singleClass.Key.FullName, BaseClass = singleClass.Key.BaseClass, ArgumentsToBaseConstructor = singleClass.Key.ArgumentsToBaseConstructor, Tests = singleClass };
        }

        IEnumerable<TestMethod> GenerateTestMethods(MethodInfo originalMethod)
        {
            TestMethodGeneratorWithContext generator = ValidateGeneratorMethod(originalMethod);
            if (generator == null)
                return new List<TestMethod>();

            IEnumerable<TestMethod> methods = generator(Context);

            if (methods.Count() == 0)
            {
                throw new NoTestsGeneratedException(originalMethod);
            }

            return
                from rawTestMethod in methods
                select ApplyDefaults(rawTestMethod, originalMethod);
        }

        TestMethodGeneratorWithContext ValidateGeneratorMethod(MethodInfo method)
        {
            logger.LogMessage(MessageImportance.Low, "Loading dynamic test method generator '{0}.{1}' ...",
                    method.DeclaringType.FullName, method.Name);
            if (!method.IsStatic)
            {
                logger.LogWarning("Dynamic test generator method '{0}.{1}' must be static!",
                    method.DeclaringType.FullName, method.Name);
                return null;
            }
            try
            {
                if (method.GetParameters().Length == 0)
                {
                    TestMethodGenerator internalGenerator = (TestMethodGenerator)Delegate.CreateDelegate(typeof(TestMethodGenerator), method, true);
                    return (context) => internalGenerator();
                }
                else
                {
                    return (TestMethodGeneratorWithContext)Delegate.CreateDelegate(typeof(TestMethodGeneratorWithContext), method, true);
                }
            }
            catch (ArgumentException)
            {
                logger.LogWarning("Could not load test generator method '{0}.{1}'! (Is the method signature correct?)",
                    method.DeclaringType.FullName, method.Name);
                return null;
            }
        }
    }
}
