//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

namespace ConfigurationAssembly
{
    using System;
    using System.Threading;
    using System.Windows.Automation;
    using System.Windows.Forms;

    public static class UIFileUploader
    {
        const string FileTextAutomationId = "1148";

        public static void ClickOpenFileButton(string browserTitle)
        {
            DateTime start = DateTime.Now;
            AutomationElement openFileButton = null;
            while (DateTime.Now - start < UICommon.Timeout)
            {
                AutomationElement browser = UICommon.GetBrowserWindow(browserTitle);
                if (browser == null)
                {
                    continue;
                }

                openFileButton = browser.FindFirst(TreeScope.Descendants,
                    new AndCondition(
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                        new PropertyCondition(AutomationElement.NameProperty, "")
                ));
                if (openFileButton != null)
                {
                    break;
                }
            }

            if (openFileButton != null)
            {
                ((InvokePattern)openFileButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
            }
        }

        static AutomationElement GetFileText(AutomationElement fileDialog)
        {
            return fileDialog.FindFirst(TreeScope.Descendants,
                new AndCondition(
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit),
                    new PropertyCondition(AutomationElement.AutomationIdProperty, FileTextAutomationId)));
        }



        public static void Upload(string browserTitle, string filePath)
        {
            const string FileDialogClassName = "#32770";
            const string OkButtonAutomationId = "1";

            AutomationElement fileDialog = null;

            try
            {
                DateTime start = DateTime.Now;
                while (DateTime.Now - start < UICommon.Timeout)
                {
                    AutomationElement browser = UICommon.GetBrowserWindow(browserTitle);
                    if (browser == null)
                        continue;
                    fileDialog = UICommon.GetPopupDialog(browser, FileDialogClassName);

                    if (fileDialog != null)
                    {
                        break;
                    }
                }

                AutomationElement fileText = GetFileText(fileDialog);
                ((ValuePattern)fileText.GetCurrentPattern(ValuePattern.Pattern)).SetValue(filePath);

                try
                {
                    AutomationElement okButton = UICommon.GetOkButton(fileDialog, OkButtonAutomationId);
                    ((InvokePattern)okButton.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
                }
                finally
                {
                    fileDialog.SetFocus();
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
                    if (fileDialog != null)
                    {
                        ((WindowPattern)fileDialog.GetCurrentPattern(WindowPattern.Pattern)).Close();
                    }
                }
                catch
                {
                }
            }
        }
    }
}
