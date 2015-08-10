using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class MeasureTypeQuery
    {
        #region const properties
        // MeasureType TABLE

        const string ID_TABLE_MEASURETYPE = "measure_type";
        const string ID_ID_MEASURETYPE = "measure_type_id";
        const string ID_DESCRIPTION_MEASURETYPE = "measure_type_description";
        const string ID_INSERTTIME_MEASURETYPE = "inserttime";
        const string ID_INSERTUSER_MEASURETYPE = "insertuser";
        const string ID_UPDATETIME_MEASURETYPE = "updatetime";
        const string ID_UPDATEUSER_MEASURETYPE = "updateuser";
        const string DATETIMEFORMAT_MEASURETYPE = "yyyyMMddHHmmss";

        const int POSITION_ID_MEASURETYPE = 0;
        const int POSITION_DESCRIPTION_MEASURETYPE = 1;
        const int POSITION_INSERTTIME_MEASURETYPE = 2;
        const int POSITION_INSERTUSER_MEASURETYPE = 3;
        const int POSITION_UPDATETIME_MEASURETYPE = 4;
        const int POSITION_UPDATEUSER_MEASURETYPE = 5;

        #endregion

        public static IList<MeasureType> ParseDataSetToMeasureType(DataTable dataTable)
        {
            IList<MeasureType> measureTypeList = new List<MeasureType>();
            foreach (DataRow row in dataTable.Rows)
            {
                MeasureType measureType = new MeasureType()
                {
                    Id = row[POSITION_ID_MEASURETYPE].ToString(),
                    Description = row[POSITION_DESCRIPTION_MEASURETYPE].ToString(),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_MEASURETYPE].ToString()),
                    InsertUser = row[POSITION_INSERTUSER_MEASURETYPE].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_MEASURETYPE].ToString()),
                    UpdateUser = row[POSITION_UPDATEUSER_MEASURETYPE].ToString()
                };
                measureTypeList.Add(measureType);
            }

            return measureTypeList;
        }

        public static string getQueryForGetMeasureType(string measureTypeId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_MEASURETYPE, ID_ID_MEASURETYPE, measureTypeId);
        }

        public static string getQueryForGetAllMeasureType()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_MEASURETYPE);
        }

        public static string getQuerySaveMeasureType(MeasureType measureType)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}')", ID_TABLE_MEASURETYPE,
                measureType.Id,
                measureType.Description,
                measureType.LocalInsertTime.ToString(DATETIMEFORMAT_MEASURETYPE),
                measureType.InsertUser,
                measureType.UpdateLocalDateTime.ToString(DATETIMEFORMAT_MEASURETYPE),
                measureType.UpdateUser);
        }

        public static string getQueryUpdateMeasureType(MeasureType measureType)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}'WHERE {9}='{10}'",
                 ID_TABLE_MEASURETYPE,
                 ID_DESCRIPTION_MEASURETYPE, measureType.Description,
                 ID_UPDATETIME_MEASURETYPE, measureType.UpdateLocalDateTime.ToString(DATETIMEFORMAT_MEASURETYPE),
                 ID_UPDATEUSER_MEASURETYPE, measureType.UpdateUser,
                 ID_ID_MEASURETYPE, measureType.Id
                );
        }


        private static DateTime getDateTime(string dateTimeString)
        {
            DateTime result;
            DateTime.TryParseExact(dateTimeString, DATETIMEFORMAT_MEASURETYPE, new CultureInfo("es-ES"), DateTimeStyles.None, out result);
            return result;
        }




        
    }
}
