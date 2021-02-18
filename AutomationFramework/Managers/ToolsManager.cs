﻿using AutomationFramework.Utils;
using Bogus;
using System;

namespace AutomationFramework.Managers
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

        protected ToolsManager(RunSettingManager runSettingManager)
        {
            _dataBase = new DataBaseHelper(runSettingManager);
            _enum = new EnumHelper();
            _string = new StringHelper();
            _api = new ApiHelper(runSettingManager, _string);
            _getFakeData = new Faker();
            _getRandom = new Random();
        }

        internal static ToolsManager GetToolsManager(RunSettingManager runSettingManager)
        {
            if (_toolsManager == null)
            {
                _toolsManager = new ToolsManager(runSettingManager);
            }

            return _toolsManager;
        }
    }
}