using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class SubCategoryQuery
    {
        #region const properties
        // SubCategory TABLE

        const string ID_TABLE_SUBCATEGORY = "middleware.subcategory";
        const string ID_ID_SUBCATEGORY = "subcategory_id";
        const string ID_CATEGORY_SUBCATEGORY = "category_id";
        const string ID_DESCRIPTION_SUBCATEGORY = "subcategory_desc";
        const string ID_INSERTTIME_SUBCATEGORY = "inserttime";
        const string ID_INSERTUSER_SUBCATEGORY = "insertuser";
        const string ID_UPDATETIME_SUBCATEGORY = "updatetime";
        const string ID_UPDATEUSER_SUBCATEGORY = "updateuser";

        const string DATETIMEFORMAT_SUBCATEGORY = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_SUBCATEGORY = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_ID_SUBCATEGORY = 0;
        const int POSITION_CATEGORY_SUBCATEGORY = 1;
        const int POSITION_DESCRIPTION_SUBCATEGORY = 2;
        const int POSITION_INSERTTIME_SUBCATEGORY = 3;
        const int POSITION_INSERTUSER_SUBCATEGORY = 4;
        const int POSITION_UPDATETIME_SUBCATEGORY = 5;
        const int POSITION_UPDATEUSER_SUBCATEGORY = 6;

        #endregion

        public static IList<SubCategory> ParseDataSetToSubCategory(DataTable dataTable)
        {
            IList<SubCategory> subcategoryList = new List<SubCategory>();
            foreach (DataRow row in dataTable.Rows)
            {
                SubCategory subcategory = new SubCategory()
                {
                    Id = row[POSITION_ID_SUBCATEGORY].ToString(),
                    Category= new Category(row[POSITION_CATEGORY_SUBCATEGORY].ToString()),
                    Description = row[POSITION_DESCRIPTION_SUBCATEGORY].ToString(),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_SUBCATEGORY].ToString(),DATETIMEFORMATINSERT_SUBCATEGORY),
                    InsertUser = row[POSITION_INSERTUSER_SUBCATEGORY].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_SUBCATEGORY].ToString(),DATETIMEFORMATINSERT_SUBCATEGORY),
                    UpdateUser = row[POSITION_UPDATEUSER_SUBCATEGORY].ToString()
                };
                subcategoryList.Add(subcategory);
            }

            return subcategoryList;
        }

        public static string getQueryForGetSubCategory(string subcategoryId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_SUBCATEGORY, ID_ID_SUBCATEGORY, subcategoryId);
        }

        public static string getQueryForGetAllSubCategory()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_SUBCATEGORY);
        }

        public static string getQuerySaveSubCategory(SubCategory subcategory)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}')", ID_TABLE_SUBCATEGORY,
                subcategory.Id,
                subcategory.Category.Id,
                subcategory.Description,
                subcategory.LocalInsertTime.ToString(DATETIMEFORMATINSERT_SUBCATEGORY),
                subcategory.InsertUser,
                subcategory.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_SUBCATEGORY),
                subcategory.UpdateUser);
        }

        public static string getQueryUpdateSubCategory(SubCategory subcategory)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}',{7}='{8}'WHERE {9}='{10}'",
                 ID_TABLE_SUBCATEGORY,
                 ID_CATEGORY_SUBCATEGORY,subcategory.Category.Id,
                 ID_DESCRIPTION_SUBCATEGORY, subcategory.Description,
                 ID_UPDATETIME_SUBCATEGORY, subcategory.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_SUBCATEGORY),
                 ID_UPDATEUSER_SUBCATEGORY, subcategory.UpdateUser,
                 ID_ID_SUBCATEGORY, subcategory.Id
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
