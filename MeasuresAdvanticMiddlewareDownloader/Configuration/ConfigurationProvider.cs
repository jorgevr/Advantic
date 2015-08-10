using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MeasuresAdvanticMiddlewareDownloader.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public void Refresh()
        {
            ConfigurationManager.RefreshSection("appSettings");
        }

        public string GetMeasuresLoaderCronExpression()
        {
            return ConfigurationManager.AppSettings["CronExpression"];
        }

        public string GetAdvanticMiddlewareParseFilePath()
        {
            return ConfigurationManager.AppSettings["AdvanticMiddlewareParseFilePath"];
        }

        public string GetPostgreSqlConnectionHost()
        {
            return ConfigurationManager.AppSettings["PostgreSqlHost"];
        }
        public string GetPostgreSqlConnectionUser()
        {
            return ConfigurationManager.AppSettings["PostgreSqlUser"];
        }
        public int GetPostgreSqlConnectionPort()
        {
            return int.Parse(ConfigurationManager.AppSettings["PostgreSqlPort"]);
        }
        public string GetPostgreSqlConnectionPassword()
        {
            return ConfigurationManager.AppSettings["PostgreSqlPassword"];
        }
        public string GetPostgreSqlConnectionDatabase()
        {
            return ConfigurationManager.AppSettings["PostgreSqlDatabase"];
        }
        public string GetAdvanticUser()
        {
            return ConfigurationManager.AppSettings["AdvanticUser"];
        }

        public DateTime GetReferenceDate()
        {
            DateTime result = DateTime.Now;
            string date = ConfigurationManager.AppSettings["ReferenceDate"].ToUpper();
            if (date == "NOW") 
                return result.Date;
            DateTime.TryParse(date, out result);
            return result;
        }
        public int GetDaysBeforeReferenceDate()
        {
            return int.Parse(ConfigurationManager.AppSettings["DaysBeforeReferenceDate"]);
        }
        public int GetDaysAfterReferenceDate()
        {
            return int.Parse(ConfigurationManager.AppSettings["DaysAfterReferenceDate"]);
        }
        

    }
}
