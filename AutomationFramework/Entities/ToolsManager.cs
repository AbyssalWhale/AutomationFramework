using AutomationFramework.Utils;
using Bogus;
using System;

namespace AutomationFramework.Entities
{
    public class ToolsManager
    {
        private static ToolsManager _toolsManager;
        public DataBaseHelper _dataBase { get; private set; }
        public EnumHelper _enum { get; private set; }
        public StringHelper _string { get; private set; }
        public ApiHelper _api { get; private set; }
        public Faker _getFakeData { get; private set; }
        public Random _getRandom { get; private set; }

        protected ToolsManager(RunSettingManager runSettingManager, LogManager logManager)
        {
            _dataBase = new DataBaseHelper(runSettingManager.DBServer, runSettingManager.DBName, runSettingManager.DBUserId, runSettingManager.DBUserPass);
            _enum = new EnumHelper();
            _string = new StringHelper();
            _api = new ApiHelper(runSettingManager, logManager, _string);
            _getFakeData = new Faker();
            _getRandom = new Random();
        }

        internal static ToolsManager GetToolsManager(RunSettingManager runSettingManager, LogManager logManager)
        {
            if (_toolsManager == null)
            {
                _toolsManager = new ToolsManager(runSettingManager, logManager);
            }

            return _toolsManager;
        }
    }
}
