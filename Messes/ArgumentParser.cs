// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace ConfigurationAssembly
{
    public class ArgumentParser
    {
        string testTarget;
        public const string testTargetToken = "/testtarget:";
        public const string configureIeToken = "/ie";
        public const string configureAllToken = "/all";
        public const string windowTitleToken = "/windowtitle:";
        public const string uploadFileToken = "/uploadfile:";
        public const string helpToken = "/?";
        public const string clickDownloadLinkToken = "/clickdownloadlink";
        public const string downloadFileToken = "/downloadfile";
        public const string connectAddInToken = "/connectaddin";
        public const string disconnectAddInToken = "/disconnectaddin";
        public const string clickupLoadButtonToken = "/clickuploadbutton";
        public const string addInProgIdToken = "/addin:";
        public const string deleteFileToken = "/delete:";
        public const string deleteDirToken = "/deletedir:";
        public const string copyFileFromToken = "/copyfrom:";
        public const string copyFileToToken = "/copyto:";
        public const string startOdataServerToken = "/startodata:";
        public const string stopOdataServerToken = "/stopodata:";
        public const string dismissErrorDialogToken = "/dismisserrordialog";
        public const string enableChromeDriverToken = "/enablechromedriver:";
        public const string disableChromeDriverToken = "/disablechromedriver:";
        public const string ignoreSecurityWarningToken = "/ignoresecuritywarning";

        bool configureIe;
        string windowTitle;
        string uploadFileName;
        bool showHelp;
        bool clickDownloadLink;
        bool downloadFile;
        bool connectAddin;
        bool disctonnectAddin;
        string AddInProgId;
        string deleteFile;
        string copyFrom;
        string copyTo;
        string deleteDir;
        string startOdata;
        string stopOdata;
        bool clickUploadButton;
        bool dismissErrorDialog;
        string enableChromeDriverFirewall;
        string disableChromeDriverFirewall;
        bool ignoreSecurityWarning;

        public ArgumentParser(string[] arguments)
        {
            Parse(arguments);
        }

        void Parse(string[] arguments)
        {
            foreach (string arg in arguments)
            {
                string lowerArg = arg.ToLower();
                if (lowerArg.StartsWith(testTargetToken))
                {
                    testTarget = lowerArg.Substring(testTargetToken.Length);
                    continue;
                }
                if (lowerArg == configureIeToken)
                {
                    configureIe = true;
                    continue;
                }
                if (lowerArg == configureAllToken)
                {
                    configureIe = true;
                    continue;
                }
                if (lowerArg.StartsWith(windowTitleToken))
                {
                    windowTitle = lowerArg.Substring(windowTitleToken.Length);
                    continue;
                }
                if (lowerArg == helpToken)
                {
                    showHelp = true;
                    continue;
                }
                if (lowerArg.StartsWith(uploadFileToken))
                {
                    uploadFileName = lowerArg.Substring(uploadFileToken.Length);
                    continue;
                }
                if (lowerArg == downloadFileToken)
                {
                    downloadFile = true;
                    continue;
                }
                if (lowerArg == connectAddInToken)
                {
                    connectAddin = true;
                    continue;
                }
                if (lowerArg == disconnectAddInToken)
                {
                    disctonnectAddin = true;
                    continue;
                }
                if (lowerArg.StartsWith(addInProgIdToken))
                {
                    AddInProgId = lowerArg.Substring(addInProgIdToken.Length);
                    continue;
                }
                if (lowerArg.StartsWith(deleteFileToken))
                {
                    deleteFile = lowerArg.Substring(deleteFileToken.Length);
                    continue;
                }
                if (lowerArg.StartsWith(deleteDirToken))
                {
                    deleteDir = lowerArg.Substring(deleteDirToken.Length);
                    continue;
                }
                if (lowerArg.StartsWith(copyFileFromToken))
                {
                    copyFrom = lowerArg.Substring(copyFileFromToken.Length);
                    continue;
                }
                if (lowerArg.StartsWith(copyFileToToken))
                {
                    copyTo = lowerArg.Substring(copyFileToToken.Length);
                    continue;
                }
                if (lowerArg.StartsWith(startOdataServerToken))
                {
                    startOdata = lowerArg.Substring(startOdataServerToken.Length);
                    continue;
                }
                if (lowerArg.StartsWith(stopOdataServerToken))
                {
                    stopOdata = lowerArg.Substring(stopOdataServerToken.Length);
                    continue;
                }
                if (lowerArg == clickupLoadButtonToken)
                {
                    clickUploadButton = true;
                    continue;
                }
                if (lowerArg == dismissErrorDialogToken)
                {
                    dismissErrorDialog = true;
                    continue;
                }
                if (lowerArg == clickDownloadLinkToken)
                {
                    clickDownloadLink = true;
                    continue;
                }
                if (lowerArg == enableChromeDriverToken)
                {
                    enableChromeDriverFirewall = lowerArg.Substring(enableChromeDriverToken.Length);
                    continue;
                }
                if (lowerArg == disableChromeDriverToken)
                {
                    disableChromeDriverFirewall = lowerArg.Substring(disableChromeDriverToken.Length);
                    continue;
                }
                if (lowerArg == ignoreSecurityWarningToken)
                {
                    ignoreSecurityWarning = true;
                    continue;
                }
            }
        }

        public string TestTarget { get { return this.testTarget; } }
        public bool ConfigureIe { get { return this.configureIe; } }
        public string WindowTitle { get { return this.windowTitle; } }
        public string UploadFieName { get { return this.uploadFileName; } }
        public bool ShowHelp { get { return this.showHelp; } }
        public bool ClickDownloadLink { get { return this.clickDownloadLink; } }
        public bool DownloadFile { get { return this.downloadFile; } }
        public bool ConnectAddin { get { return this.connectAddin; } }
        public bool DisconnectAddin { get { return this.disctonnectAddin; } }
        public string AddinProgId { get { return this.AddInProgId; } }
        public string DeleteFile { get { return this.deleteFile; } }
        public string DeleteDir { get { return this.deleteDir; } }
        public string CopyFrom { get { return this.copyFrom; } }
        public string CopyTo { get { return this.copyTo; } }
        public string StartOdata { get { return this.startOdata;} }
        public string StopOdata { get { return this.stopOdata; } }
        public bool ClickUploadButton { get { return this.clickUploadButton; } }
        public bool DismissErrorDialog { get { return this.dismissErrorDialog; } }
        public string EnableChromeDriverFirewall { get { return this.enableChromeDriverFirewall; } }
        public string DisableChromeDriverFirewall { get { return this.disableChromeDriverFirewall; } }
        public bool IgnoreSecurityWarning { get { return this.ignoreSecurityWarning; } }
    }
}