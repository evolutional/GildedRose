//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

namespace ConfigurationAssembly
{
    using System;
    using System.Windows.Automation;
    using System.Windows.Forms;

    public static class UICommon
    {
        public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(300);

        public static AutomationElement GetPopupDialog(AutomationElement parent, string DialogClassName)
        {
            return parent.FindFirst(TreeScope.Children,
                new PropertyCondition(AutomationElement.ClassNameProperty, DialogClassName));
        }


        public static AutomationElement GetOkButton(AutomationElement fileDialog, string OkButtonAutomationId)
        {
            return fileDialog.FindFirst(TreeScope.Descendants,
                new AndCondition(
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                    new PropertyCondition(AutomationElement.AutomationIdProperty, OkButtonAutomationId)));
        }

        public static AutomationElement GetBrowserWindow(string browserTitle)
        {
            AutomationElementCollection topWindows =
            AutomationElement.RootElement.FindAll(TreeScope.Children, Condition.TrueCondition);

            foreach (AutomationElement window in topWindows)
            {
                if (window.Current.Name.Replace("\"", "").ToLower().Contains(browserTitle))
                {
                    return window;
                }
            }
            return null;
        }

        public static void DismissErrorDialog(string browserTitle)
        {
            const string ErrorDialogClassName = "#32770";
            const string OKButtonErrorDialogAutomationId = "2";


            AutomationElement errorDialog = null;

            try
            {
                DateTime start = DateTime.Now;
                while (DateTime.Now - start < UICommon.Timeout)
                {
                    AutomationElement browser = UICommon.GetBrowserWindow(browserTitle);
                    if (browser == null)
                    {
                        continue;
                    }
                    errorDialog = GetPopupDialog(browser, ErrorDialogClassName);

                    if (errorDialog != null)
                    {
                        break;
                    }
                }

                try
                {
                    AutomationElement okButton = GetOkButton(errorDialog, OKButtonErrorDialogAutomationId);
                    ((InvokePattern)okButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
                }
                finally
                {
                    errorDialog.SetFocus();
                    SendKeys.SendWait("{ENTER}");
                }
            }
            catch
            {
            }
            finally
            {
                try
                {
                    if (errorDialog != null)
                    {
                        ((WindowPattern)errorDialog.GetCurrentPattern(WindowPattern.Pattern)).Close();
                    }
                }
                catch
                {
                }
            }
        }

        public static AutomationElement FindElement(Func<AutomationElement> f)
        {
            AutomationElement result;
            DateTime start = DateTime.Now;
            while (DateTime.Now - start < UICommon.Timeout)
            {
                result = f();
                if (result != null)
                {
                    return result;
                }
            }

            throw new Exception("Failed to find element");
        }
    }
}
