using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class MeasureQuery
    {
        #region const properties
        // MEASURE TABLE
        const string ID_TABLE_MEASURE = "middleware.h_measure";
        const string ID_LOCALDATETIME_MEASURE = "h_measure_time";
        const string ID_DEVICE_MEASURE = "device_id";
        const string ID_UNITTYPE_MEASURE = "unit_type_id";
        const string ID_MEASURETYPE_MEASURE = "measure_type_id";
        const string ID_VALUE_MEASURE = "h_measure_value";
        const string ID_INSERTTIME_MEASURE = "inserttime";
        const string ID_INSERTUSER_MEASURE = "insertuser";
        const string ID_UPDATETIME_MEASURE = "updatetime";
        const string ID_UPDATEUSER_MEASURE = "updateuser";
        const string DATETIMEFORMAT_MEASURE = "yyyyMMddHHmm";
        const string DATETIMEFORMATINSERT_MEASURE = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_LOCALDATETIME_MEASURE = 0;
        const int POSITION_DEVICE_MEASURE = 2;
        const int POSITION_UNITTYPE_MEASURE = 3;
        const int POSITION_MEASURETYPE_MEASURE = 4;
        const int POSITION_VALUE_MEASURE = 1;
        const int POSITION_INSERTTIME_MEASURE = 5;
        const int POSITION_INSERTUSER_MEASURE = 6;
        const int POSITION_UPDATETIME_MEASURE = 7;
        const int POSITION_UPDATEUSER_MEASURE = 8;

        #endregion

        public static IList<Measure> ParseDataSetToMeasure(DataTable dataTable)
        {
            IList<Measure> measureList = new List<Measure>();
            foreach (DataRow row in dataTable.Rows)
            {
                Measure measure = new Measure()
                {
                    LocalDateTime = getDateTime(row[POSITION_LOCALDATETIME_MEASURE].ToString(), DATETIMEFORMAT_MEASURE),
                    DeviceId = row[POSITION_DEVICE_MEASURE].ToString(),
                    UnitTypeId = row[POSITION_UNITTYPE_MEASURE].ToString(),
                    MeasureTypeId = row[POSITION_MEASURETYPE_MEASURE].ToString(),
                    Value = Double.Parse(row[POSITION_VALUE_MEASURE].ToString()),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_MEASURE].ToString(), DATETIMEFORMATINSERT_MEASURE),
                    InsertUser = row[POSITION_INSERTUSER_MEASURE].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_MEASURE].ToString(), DATETIMEFORMATINSERT_MEASURE),
                    UpdateUser = row[POSITION_UPDATEUSER_MEASURE].ToString()
                };
                measureList.Add(measure);
            }

            return measureList;
        }

        public static string getQueryForGetAllMeasure()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_MEASURE);
        }

        public static string getQueryForGetMeasure(string deviceId, string typeId, DateTime localDatetime)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}' AND {3}='{4}' AND {5}='{6}'", ID_TABLE_MEASURE, ID_DEVICE_MEASURE, deviceId, ID_MEASURETYPE_MEASURE, typeId, ID_LOCALDATETIME_MEASURE, localDatetime.ToString(DATETIMEFORMAT_MEASURE));
        }

        public static string getQueryByDeviceTypeAndDateTimeRange(string deviceId, string typeId, DateTime startLocalDateTime, DateTime endLocalDateTime)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}' AND {3}='{4}' AND {5}>='{6}' AND {5}<='{7}'", ID_TABLE_MEASURE, ID_DEVICE_MEASURE, deviceId, ID_MEASURETYPE_MEASURE, typeId, ID_LOCALDATETIME_MEASURE, startLocalDateTime.ToString(DATETIMEFORMAT_MEASURE), endLocalDateTime.ToString(DATETIMEFORMAT_MEASURE));
        }

        public static string getQuerySaveMeasure(Measure measure)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}'::timestamp without time zone,'{7}','{8}'::timestamp without time zone,'{9}')", ID_TABLE_MEASURE, measure.LocalDateTime.ToString(DATETIMEFORMAT_MEASURE),
                measure.Value,measure.DeviceId, measure.UnitTypeId, measure.MeasureTypeId, measure.LocalInsertTime,
                measure.InsertUser, measure.UpdateLocalDateTime, measure.UpdateUser);
        }

        public static string getQueryUpdateMeasure(Measure measure)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}'::timestamp without time zone WHERE {7}='{8}' AND {9}='{10}' AND {11}='{12}' AND {13}='{14}'::timestamp without time zone AND {15}={16}",
                ID_TABLE_MEASURE,
                ID_VALUE_MEASURE, measure.Value, ID_UPDATEUSER_MEASURE, measure.UpdateUser, ID_UPDATETIME_MEASURE, measure.UpdateLocalDateTime,
                ID_DEVICE_MEASURE, measure.DeviceId,
                ID_UNITTYPE_MEASURE, measure.UnitTypeId,
                ID_LOCALDATETIME_MEASURE, measure.LocalDateTime.ToString(DATETIMEFORMAT_MEASURE),
                ID_INSERTTIME_MEASURE, measure.LocalInsertTime,
                ID_INSERTUSER_MEASURE, measure.InsertUser);
        }

        private static DateTime getDateTime(string dateTimeString, string format)
        {
            DateTime result;
            DateTime.TryParseExact(dateTimeString, format, new CultureInfo("es-ES"), DateTimeStyles.None, out result);
            return result;
        }

    }
}
