using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class CurrencyQuery
    {
        #region const properties
        // UnitType TABLE

        const string ID_TABLE_CURRENCY = "middleware.currency";
        const string ID_ID_CURRENCY = "unit_type_id";
        const string ID_DESCRIPTION_CURRENCY = "unit_type_desc";
        const string ID_INSERTTIME_CURRENCY = "inserttime";
        const string ID_INSERTUSER_CURRENCY = "insertuser";
        const string ID_UPDATETIME_CURRENCY = "updatetime";
        const string ID_UPDATEUSER_CURRENCY = "updateuser";
        const string DATETIMEFORMAT_CURRENCY = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_CURRENCY = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_ID_CURRENCY = 0;
        const int POSITION_DESCRIPTION_CURRENCY = 1;
        const int POSITION_INSERTTIME_CURRENCY = 2;
        const int POSITION_INSERTUSER_CURRENCY = 3;
        const int POSITION_UPDATETIME_CURRENCY = 4;
        const int POSITION_UPDATEUSER_CURRENCY = 5;

        #endregion

        public static IList<Currency> ParseDataSetToCurrency(DataTable dataTable)
        {
            IList<Currency> currencyList = new List<Currency>();
            foreach (DataRow row in dataTable.Rows)
            {
                Currency currency = new Currency()
                {
                    Id = row[POSITION_ID_CURRENCY].ToString(),
                    Description = row[POSITION_DESCRIPTION_CURRENCY].ToString(),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_CURRENCY].ToString(), DATETIMEFORMATINSERT_CURRENCY),
                    InsertUser = row[POSITION_INSERTUSER_CURRENCY].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_CURRENCY].ToString(), DATETIMEFORMATINSERT_CURRENCY),
                    UpdateUser = row[POSITION_UPDATEUSER_CURRENCY].ToString()
                };
                currencyList.Add(currency);
            }

            return currencyList;
        }

        public static string getQueryForGetCurrency(string currencyId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_CURRENCY, ID_ID_CURRENCY, currencyId);
        }

        public static string getQueryForGetAllCurrency()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_CURRENCY);
        }

        public static string getQuerySaveCurrency(Currency currency)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}')", ID_TABLE_CURRENCY,
                currency.Id,
                currency.Description,
                currency.LocalInsertTime.ToString(DATETIMEFORMATINSERT_CURRENCY),
                currency.InsertUser,
                currency.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_CURRENCY),
                currency.UpdateUser);
        }

        public static string getQueryUpdateCurrency(Currency currency)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}'WHERE {9}='{10}'",
                 ID_TABLE_CURRENCY,
                 ID_DESCRIPTION_CURRENCY, currency.Description,
                 ID_UPDATETIME_CURRENCY, currency.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_CURRENCY),
                 ID_UPDATEUSER_CURRENCY, currency.UpdateUser,
                 ID_ID_CURRENCY, currency.Id
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
