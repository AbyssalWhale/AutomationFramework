using AutomationFramework.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomationFramework.Entities
{
    public class UtilsManager
    {
        public DataBaseHelper DataBase { get; private set; }
        public EnumHelper Enum { get; private set; }
        public StringHelper String { get; private set; }
        public ApiHelper API { get; private set; }

        public UtilsManager(RunSettingManager runSettingManager, LogManager logManager)
        {
            DataBase = new DataBaseHelper(runSettingManager.DBServer, runSettingManager.DBName, runSettingManager.DBUserId, runSettingManager.DBUserPass);
            Enum = new EnumHelper();
            String = new StringHelper();
            API = new ApiHelper(runSettingManager, logManager);
        }
    }
}
