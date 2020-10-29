using AutomationFramework.Entities;
using AutomationFramework.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using TestsBaseConfigurator.Interfaces;

namespace TestsBaseConfigurator.POM
{
    public abstract class BasePagePOM
    {
        protected WebDriverManager _webDriverManager { get; }
        protected RunSettingManager _runSettingsSettings { get; }
        protected LogManager _logManager { get; }
        protected FolderManager _folderManager { get; }
        protected UtilsManager _utilsManager { get; }

        public abstract string Title { get; }
        protected abstract bool IsAt();
        protected BasePagePOM(
            WebDriverManager webDriverManager, 
            RunSettingManager runSettingManager, 
            LogManager logManager, 
            FolderManager folderManager,
            UtilsManager utilsManager)
        {
            _webDriverManager = webDriverManager;
            _runSettingsSettings = runSettingManager;
            _logManager = logManager;
            _folderManager = folderManager;
            _utilsManager = utilsManager;

            _logManager.LogAction(LogLevels.local, $"Initializing the '{Title}' page");
        }
    }
}
