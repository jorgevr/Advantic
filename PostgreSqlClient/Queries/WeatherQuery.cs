using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class WeatherQuery
    {
        #region const properties
        // MEASURE TABLE
        const string ID_TABLE_WEATHER = "middleware.h_weather";
        const string ID_LOCALDATETIME_WEATHER = "h_weather_time";
        const string ID_MACROCELL_WEATHER = "macrocell_id";
        const string ID_UNITTYPE_WEATHER = "unit_type_id";
        const string ID_MEASURETYPE_WEATHER = "measure_type_id";
        const string ID_VALUE_WEATHER = "h_weather_value";
        const string ID_INSERTTIME_WEATHER = "inserttime";
        const string ID_INSERTUSER_WEATHER = "insertuser";
        const string ID_UPDATETIME_WEATHER = "updatetime";
        const string ID_UPDATEUSER_WEATHER = "updateuser";
        const string DATETIMEFORMAT_WEATHER = "yyyyMMddHHmm";
        const string DATETIMEFORMATINSERT_WEATHER = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_LOCALDATETIME_WEATHER = 0;
        const int POSITION_MACROCELL_WEATHER= 4;
        const int POSITION_UNITTYPE_WEATHER = 2;
        const int POSITION_MEASURETYPE_WEATHER = 3;
        const int POSITION_VALUE_WEATHER = 1;
        const int POSITION_INSERTTIME_WEATHER = 5;
        const int POSITION_INSERTUSER_WEATHER = 6;
        const int POSITION_UPDATETIME_WEATHER = 7;
        const int POSITION_UPDATEUSER_WEATHER = 8;

        #endregion

        public static IList<Weather> ParseDataSetToWeather(DataTable dataTable)
        {
            IList<Weather> weatherList = new List<Weather>();
            foreach (DataRow row in dataTable.Rows)
            {
                Weather weather = new Weather()
                {
                    LocalDateTime = getDateTime(row[POSITION_LOCALDATETIME_WEATHER].ToString(), DATETIMEFORMAT_WEATHER),
                    MacrocellId = row[POSITION_MACROCELL_WEATHER].ToString(),
                    UnitTypeId = row[POSITION_UNITTYPE_WEATHER].ToString(),
                    MeasureTypeId = row[POSITION_MEASURETYPE_WEATHER].ToString(),
                    Value = Double.Parse(row[POSITION_VALUE_WEATHER].ToString()),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_WEATHER].ToString(), DATETIMEFORMATINSERT_WEATHER),
                    InsertUser = row[POSITION_INSERTUSER_WEATHER].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_WEATHER].ToString(), DATETIMEFORMATINSERT_WEATHER),
                    UpdateUser = row[POSITION_UPDATEUSER_WEATHER].ToString()
                };
                weatherList.Add(weather);
            }

            return weatherList;
        }

        public static string getQueryForGetAllWeather()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_WEATHER);
        }

        public static string getQueryForGetWeather(string macrocellId, string typeId, DateTime localDatetime)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}' AND {3}='{4}' AND {5}='{6}'", ID_TABLE_WEATHER, ID_MACROCELL_WEATHER, macrocellId, ID_MEASURETYPE_WEATHER, typeId, ID_LOCALDATETIME_WEATHER, localDatetime.ToString(DATETIMEFORMAT_WEATHER));
        }

        public static string getQueryByMacrocellTypeAndDateTimeRange(string macrocellId, string typeId, DateTime startLocalDateTime, DateTime endLocalDateTime)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}' AND {3}='{4}' AND {5}>='{6}' AND {5}<='{7}'", ID_TABLE_WEATHER, ID_MACROCELL_WEATHER, macrocellId, ID_MEASURETYPE_WEATHER, typeId, ID_LOCALDATETIME_WEATHER, startLocalDateTime.ToString(DATETIMEFORMAT_WEATHER), endLocalDateTime.ToString(DATETIMEFORMAT_WEATHER));
        }

        public static string getQuerySaveWeather(Weather weather)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}'::timestamp without time zone,'{7}','{8}'::timestamp without time zone,'{9}')", ID_TABLE_WEATHER, weather.LocalDateTime.ToString(DATETIMEFORMAT_WEATHER),
                weather.Value,weather.UnitTypeId, weather.MeasureTypeId, weather.MacrocellId, weather.LocalInsertTime,
                weather.InsertUser, weather.UpdateLocalDateTime, weather.UpdateUser);
        }

        public static string getQueryUpdateWeather(Weather weather)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}'::timestamp without time zone WHERE {7}='{8}' AND {9}='{10}' AND {11}='{12}' AND {13}='{14}'::timestamp without time zone AND {15}={16}",
                ID_TABLE_WEATHER,
                ID_VALUE_WEATHER, weather.Value, ID_UPDATEUSER_WEATHER, weather.UpdateUser, ID_UPDATETIME_WEATHER, weather.UpdateLocalDateTime,
                ID_MACROCELL_WEATHER, weather.MacrocellId,
                ID_UNITTYPE_WEATHER, weather.UnitTypeId,
                ID_LOCALDATETIME_WEATHER, weather.LocalDateTime.ToString(DATETIMEFORMAT_WEATHER),
                ID_INSERTTIME_WEATHER, weather.LocalInsertTime,
                ID_INSERTUSER_WEATHER, weather.InsertUser);
        }

        private static DateTime getDateTime(string dateTimeString, string format)
        {
            DateTime result;
            DateTime.TryParseExact(dateTimeString, format, new CultureInfo("es-ES"), DateTimeStyles.None, out result);
            return result;
        }

    }
}
