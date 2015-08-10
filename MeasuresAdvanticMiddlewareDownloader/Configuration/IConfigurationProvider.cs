using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuresAdvanticMiddlewareDownloader.Configuration
{
    public interface IConfigurationProvider
    {
        void Refresh();
        string GetMeasuresLoaderCronExpression();
        string GetAdvanticMiddlewareParseFilePath();
        string GetPostgreSqlConnectionHost();
        string GetPostgreSqlConnectionUser();
        int GetPostgreSqlConnectionPort();
        string GetPostgreSqlConnectionPassword();
        string GetPostgreSqlConnectionDatabase();
        string GetAdvanticUser();
        DateTime GetReferenceDate();
        int GetDaysBeforeReferenceDate();
        int GetDaysAfterReferenceDate();
    }
}
