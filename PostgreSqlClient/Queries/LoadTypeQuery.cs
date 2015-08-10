using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class LoadTypeQuery
    {
        #region const properties
        // LoadType TABLE

        const string ID_TABLE_LOADTYPE = "middleware.load_type";
        const string ID_ID_LOADTYPE = "load_type_id";
        const string ID_DESCRIPTION_LOADTYPE = "load_type_desc";
        const string ID_INSERTTIME_LOADTYPE = "inserttime";
        const string ID_INSERTUSER_LOADTYPE = "insertuser";
        const string ID_UPDATETIME_LOADTYPE = "updatetime";
        const string ID_UPDATEUSER_LOADTYPE = "updateuser";

        const string DATETIMEFORMAT_LOADTYPE = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_LOADTYPE = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_ID_LOADTYPE = 0;
        const int POSITION_DESCRIPTION_LOADTYPE = 1;
        const int POSITION_INSERTTIME_LOADTYPE = 2;
        const int POSITION_INSERTUSER_LOADTYPE = 3;
        const int POSITION_UPDATETIME_LOADTYPE = 4;
        const int POSITION_UPDATEUSER_LOADTYPE = 5;

        #endregion

        public static IList<LoadType> ParseDataSetToLoadType(DataTable dataTable)
        {
            IList<LoadType> loadTypeList = new List<LoadType>();
            foreach (DataRow row in dataTable.Rows)
            {
                LoadType loadType = new LoadType()
                {
                    Id = row[POSITION_ID_LOADTYPE].ToString(),
                    Description = row[POSITION_DESCRIPTION_LOADTYPE].ToString(),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_LOADTYPE].ToString(), DATETIMEFORMATINSERT_LOADTYPE),
                    InsertUser = row[POSITION_INSERTUSER_LOADTYPE].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_LOADTYPE].ToString(), DATETIMEFORMATINSERT_LOADTYPE),
                    UpdateUser = row[POSITION_UPDATEUSER_LOADTYPE].ToString()
                };
                loadTypeList.Add(loadType);
            }

            return loadTypeList;
        }

        public static string getQueryForGetLoadType(string loadTypeId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_LOADTYPE, ID_ID_LOADTYPE, loadTypeId);
        }

        public static string getQueryForGetAllLoadType()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_LOADTYPE);
        }

        public static string getQuerySaveLoadType(LoadType loadType)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}')", ID_TABLE_LOADTYPE,
                loadType.Id,
                loadType.Description,
                loadType.LocalInsertTime.ToString(DATETIMEFORMATINSERT_LOADTYPE),
                loadType.InsertUser,
                loadType.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_LOADTYPE),
                loadType.UpdateUser);
        }

        public static string getQueryUpdateLoadType(LoadType loadType)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}'WHERE {7}='{8}'",
                 ID_TABLE_LOADTYPE,
                 ID_DESCRIPTION_LOADTYPE, loadType.Description,
                 ID_UPDATETIME_LOADTYPE, loadType.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_LOADTYPE),
                 ID_UPDATEUSER_LOADTYPE, loadType.UpdateUser,
                 ID_ID_LOADTYPE, loadType.Id
                );
        }


        private static DateTime getDateTime(string dateTimeString, string format)
        {
            DateTime result;
            DateTime.TryParseExact(dateTimeString, format, new CultureInfo("es-ES"), DateTimeStyles.None, out result);
            return result;
        }




        
    }
}
