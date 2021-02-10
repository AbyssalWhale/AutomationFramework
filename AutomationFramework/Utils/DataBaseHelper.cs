using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AutomationFramework.Managers;
using Dapper;
using NUnit.Framework;

namespace AutomationFramework.Utils
{
    public class DataBaseHelper
    {
        public const int CommandTimeOut = 600;
        private IDbConnection DataBaseConnectionString;
        public DataBaseHelper(RunSettingManager runSettingManager)
        {
            DataBaseConnectionString = new SqlConnection(
                $"Server={runSettingManager.DBServer};" +
                $"Database={runSettingManager.DBName};" +
                $"User Id={runSettingManager.Username};" +
                $"Password={runSettingManager.Password}");
        }
        public List<T> GetDataFromDb<T>(string yourQuery)
        {
            List<T> results = new List<T>();

            try
            {
                results = DataBaseConnectionString.Query<T>(yourQuery, commandTimeout: CommandTimeOut).ToList();
            }
            catch (System.Exception ex)
            {
                Assert.IsNull($"Ex after attempting get data from Data Base. {ex.Message}");
            }
            return results;
        }

        public void RunDBQuery(string yourQuery)
        {
            DataBaseConnectionString.Query(yourQuery, commandTimeout: CommandTimeOut);
        }

        public static string TrimStringForPkey(string notTrimedString)
        {
            if (!string.IsNullOrEmpty(notTrimedString))
            {
                int startIndex = notTrimedString.IndexOf(" '") + 2;
                return notTrimedString.Substring(startIndex, notTrimedString.Length - startIndex - 2);
            }
            else
            {
                Assert.IsNull("String for trimming is empty ");
            }

            return null;
        }
    }
}
