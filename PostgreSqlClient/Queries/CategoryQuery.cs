using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class CategoryQuery
    {
        #region const properties
        // Category TABLE

        const string ID_TABLE_CATEGORY = "middleware.category";
        const string ID_ID_CATEGORY = "category_id";
        const string ID_SUPERCATEGORY_CATEGORY = "supercategory_id";
        const string ID_DESCRIPTION_CATEGORY = "category_desc";
        const string ID_INSERTTIME_CATEGORY = "inserttime";
        const string ID_INSERTUSER_CATEGORY = "insertuser";
        const string ID_UPDATETIME_CATEGORY = "updatetime";
        const string ID_UPDATEUSER_CATEGORY = "updateuser";

        const string DATETIMEFORMAT_CATEGORY = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_CATEGORY = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_ID_CATEGORY = 0;
        const int POSITION_SUPERCATEGORY_CATEGORY = 1;
        const int POSITION_DESCRIPTION_CATEGORY = 2;
        const int POSITION_INSERTTIME_CATEGORY = 3;
        const int POSITION_INSERTUSER_CATEGORY = 4;
        const int POSITION_UPDATETIME_CATEGORY = 5;
        const int POSITION_UPDATEUSER_CATEGORY = 6;

        #endregion

        public static IList<Category> ParseDataSetToCategory(DataTable dataTable)
        {
            IList<Category> categoryList = new List<Category>();
            foreach (DataRow row in dataTable.Rows)
            {
                Category category = new Category()
                {
                    Id = row[POSITION_ID_CATEGORY].ToString(),
                    SuperCategory= new SuperCategory(row[POSITION_SUPERCATEGORY_CATEGORY].ToString()),
                    Description = row[POSITION_DESCRIPTION_CATEGORY].ToString(),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_CATEGORY].ToString(),DATETIMEFORMATINSERT_CATEGORY),
                    InsertUser = row[POSITION_INSERTUSER_CATEGORY].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_CATEGORY].ToString(),DATETIMEFORMATINSERT_CATEGORY),
                    UpdateUser = row[POSITION_UPDATEUSER_CATEGORY].ToString()
                };
                categoryList.Add(category);
            }

            return categoryList;
        }

        public static string getQueryForGetCategory(string categoryId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_CATEGORY, ID_ID_CATEGORY, categoryId);
        }

        public static string getQueryForGetAllCategory()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_CATEGORY);
        }

        public static string getQuerySaveCategory(Category category)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}')", ID_TABLE_CATEGORY,
                category.Id,
                category.SuperCategory.Id,
                category.Description,
                category.LocalInsertTime.ToString(DATETIMEFORMATINSERT_CATEGORY),
                category.InsertUser,
                category.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_CATEGORY),
                category.UpdateUser);
        }

        public static string getQueryUpdateCategory(Category category)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}',{7}='{8}'WHERE {9}='{10}'",
                 ID_TABLE_CATEGORY,
                 ID_SUPERCATEGORY_CATEGORY,category.SuperCategory.Id,
                 ID_DESCRIPTION_CATEGORY, category.Description,
                 ID_UPDATETIME_CATEGORY, category.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_CATEGORY),
                 ID_UPDATEUSER_CATEGORY, category.UpdateUser,
                 ID_ID_CATEGORY, category.Id
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
