using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class SuperCategoryQuery
    {
        #region const properties
        // SuperCategory TABLE

        const string ID_TABLE_SUPERCATEGORY = "middleware.supercategory";
        const string ID_ID_SUPERCATEGORY = "supercategory_id";
        const string ID_DESCRIPTION_SUPERCATEGORY = "supercategory_desc";
        const string ID_INSERTTIME_SUPERCATEGORY = "inserttime";
        const string ID_INSERTUSER_SUPERCATEGORY = "insertuser";
        const string ID_UPDATETIME_SUPERCATEGORY = "updatetime";
        const string ID_UPDATEUSER_SUPERCATEGORY = "updateuser";

        const string DATETIMEFORMAT_SUPERCATEGORY = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_SUPERCATEGORY = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_ID_SUPERCATEGORY = 0;
        const int POSITION_DESCRIPTION_SUPERCATEGORY = 1;
        const int POSITION_INSERTTIME_SUPERCATEGORY = 2;
        const int POSITION_INSERTUSER_SUPERCATEGORY = 3;
        const int POSITION_UPDATETIME_SUPERCATEGORY = 4;
        const int POSITION_UPDATEUSER_SUPERCATEGORY = 5;

        #endregion

        public static IList<SuperCategory> ParseDataSetToSuperCategory(DataTable dataTable)
        {
            IList<SuperCategory> superCategoryList = new List<SuperCategory>();
            foreach (DataRow row in dataTable.Rows)
            {
                SuperCategory superCategory = new SuperCategory()
                {
                    Id = row[POSITION_ID_SUPERCATEGORY].ToString(),
                    Description = row[POSITION_DESCRIPTION_SUPERCATEGORY].ToString(),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_SUPERCATEGORY].ToString(), DATETIMEFORMATINSERT_SUPERCATEGORY),
                    InsertUser = row[POSITION_INSERTUSER_SUPERCATEGORY].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_SUPERCATEGORY].ToString(), DATETIMEFORMATINSERT_SUPERCATEGORY),
                    UpdateUser = row[POSITION_UPDATEUSER_SUPERCATEGORY].ToString()
                };
                superCategoryList.Add(superCategory);
            }

            return superCategoryList;
        }

        public static string getQueryForGetSuperCategory(string superCategoryId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_SUPERCATEGORY, ID_ID_SUPERCATEGORY, superCategoryId);
        }

        public static string getQueryForGetAllSuperCategory()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_SUPERCATEGORY);
        }

        public static string getQuerySaveSuperCategory(SuperCategory superCategory)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}')", ID_TABLE_SUPERCATEGORY,
                superCategory.Id,
                superCategory.Description,
                superCategory.LocalInsertTime.ToString(DATETIMEFORMATINSERT_SUPERCATEGORY),
                superCategory.InsertUser,
                superCategory.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_SUPERCATEGORY),
                superCategory.UpdateUser);
        }

        public static string getQueryUpdateSuperCategory(SuperCategory superCategory)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}'WHERE {7}='{8}'",
                 ID_TABLE_SUPERCATEGORY,
                 ID_DESCRIPTION_SUPERCATEGORY, superCategory.Description,
                 ID_UPDATETIME_SUPERCATEGORY, superCategory.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_SUPERCATEGORY),
                 ID_UPDATEUSER_SUPERCATEGORY, superCategory.UpdateUser,
                 ID_ID_SUPERCATEGORY, superCategory.Id
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
