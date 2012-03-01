//----------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//----------------------------------------------------------------

namespace ConfigurationAssembly
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Automation;

    public static class UIFileDownloader
    {
        const string NotificationbarClassName = "Frame Notification Bar";


        static AutomationElement GetNotificationBar(AutomationElement parent)
        {
            return parent.FindFirst(TreeScope.Children,
                            new PropertyCondition(AutomationElement.ClassNameProperty, NotificationbarClassName));
        }

        static AutomationElement GetButtons(AutomationElement parent)
        {
            return parent.FindFirst(TreeScope.Children,
                            new PropertyCondition(AutomationElement.ClassNameProperty, "DirectUIHWND"));
        }

        static AutomationElement GetOpenFolderButton(AutomationElement parent)
        {
            return parent.FindFirst(TreeScope.Children,
                            new PropertyCondition(AutomationElement.NameProperty,"Open folder"));
        }

        public static void ClickDownloadLink(string browserTitle)
        {
            DateTime start = DateTime.Now;
            AutomationElement downloadLink = null;
            while (DateTime.Now - start < UICommon.Timeout)
            {
                AutomationElement browser = UICommon.GetBrowserWindow(browserTitle);
                if (browser == null)
                {
                    continue;
                }

                downloadLink = browser.FindFirst(TreeScope.Descendants,
                    new AndCondition(
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Hyperlink),
                        new PropertyCondition(AutomationElement.NameProperty, "Download")
                ));
                if (downloadLink != null)
                {
                    break;
                }
            }

            if (downloadLink != null)
            {
                ((InvokePattern)downloadLink.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
            }
        }

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

        public static void Download(string browserTitle, bool ignoreSecurityWarning)
        {
            AutomationElement fileDialog = null;
            try
            {
                DateTime start = DateTime.Now;
                AutomationElement browser = UICommon.FindElement(() =>
                    {
                        return UICommon.GetBrowserWindow(browserTitle);
                    });
                fileDialog = UICommon.FindElement(() =>
                    {
                        return GetNotificationBar(browser);
                    });

                AutomationElement buttons = GetButtons(fileDialog);

                try
                {
                    FindAndClickButton(buttons, "Save");
                    if (ignoreSecurityWarning)
                    {
                        // The UI has asked if you're sure you want to open this dangerous file.
                        FindAndClickButton(buttons, "Open");
                    }
                    else
                    {
                        // The Open folder button will show when the download is complete.
                        UICommon.FindElement(() =>
                        {
                            return buttons.FindFirst(TreeScope.Children,
                                new PropertyCondition(AutomationElement.NameProperty, "Open folder"));
                        });
                    }

                    // Close the download window so that we can do multiple downloads without Download returning early.
                    FindAndClickButton(buttons, "Close");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Save Exception : {0}", ex);
                }
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

        [SuppressMessage("Microsoft.Monaco.Test", "DT0004:DoNotCallThreadSleep")]
        static void FindAndClickButton(AutomationElement buttons, string buttonName)
        {
            Console.Out.WriteLine("Clicking button '{0}'.", buttonName);

            AutomationElement button =
                UICommon.FindElement(() =>
                        {
                            return buttons.FindFirst(TreeScope.Children,
                                new PropertyCondition(AutomationElement.NameProperty, buttonName));
                        });
            AutomationPattern[] patterns = button.GetSupportedPatterns();
            System.Threading.Thread.Sleep(500);
            ((InvokePattern)button.GetCurrentPattern(InvokePattern.Pattern)).Invoke();
        }
    }
}
