using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class MacroCellQuery
    {
        #region const properties
        // DEVICE TABLE

        const string ID_TABLE_MACROCELL = "middleware.macrocell";
        const string ID_MACROCELID_MACROCELL = "macrocell_id";
        const string ID_DESCRIPTION_MACROCELL = "macrocell_desc";
        const string ID_REGION_MACROCELL = "region";
        const string ID_INSERTTIME_DEVICE = "inserttime";
        const string ID_INSERTUSER_DEVICE = "insertuser";
        const string ID_UPDATETIME_DEVICE = "updatetime";
        const string ID_UPDATEUSER_DEVICE = "updateuser";
        const string ID_CURRENCY_MACROCELL = "currency_id";

        const string DATETIMEFORMAT_MACROCELL = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_MACROCELL = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_MACROCELLID_MACROCELL = 0;
        const int POSITION_DESCRIPTION_MACROCELL = 1;
        const int POSITION_REGION_MACROCELL = 2;
        const int POSITION_INSERTTIME_MACROCELL = 3;
        const int POSITION_INSERTUSER_MACROCELL = 4;
        const int POSITION_UPDATETIME_MACROCELL = 5;
        const int POSITION_UPDATEUSER_MACROCELL = 6;
        const int POSITION_CURRENCY_MACROCELL = 7;

        #endregion

        public static IList<MacroCell> ParseDataSetToMacroCell(DataTable dataTable)
        {
            IList<MacroCell> macroCellList = new List<MacroCell>();
            foreach (DataRow row in dataTable.Rows)
            {
                MacroCell macroCell = new MacroCell()
                {
                    Id = row[POSITION_MACROCELLID_MACROCELL].ToString(),
                    Description = row[POSITION_DESCRIPTION_MACROCELL].ToString(),
                    Region = row[POSITION_REGION_MACROCELL].ToString(),
                    Currency=new Currency(row[POSITION_CURRENCY_MACROCELL].ToString()),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_MACROCELL].ToString(), DATETIMEFORMATINSERT_MACROCELL),
                    InsertUser = row[POSITION_INSERTUSER_MACROCELL].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_MACROCELL].ToString(), DATETIMEFORMATINSERT_MACROCELL),
                    UpdateUser = row[POSITION_UPDATEUSER_MACROCELL].ToString()
                };
                macroCellList.Add(macroCell);
            }

            return macroCellList;
        }

        public static string getQueryForGetMacroCell(String macroCellId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_MACROCELL, ID_MACROCELID_MACROCELL, macroCellId);
        }

        public static string getQueryForGetAllMacroCell()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_MACROCELL);
        }

        public static string getQuerySaveMacroCell(MacroCell macrocell)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", ID_TABLE_MACROCELL, 
                macrocell.Id,
                macrocell.Description,
                macrocell.Region,
                macrocell.Currency.Id,
                macrocell.LocalInsertTime.ToString(DATETIMEFORMATINSERT_MACROCELL),
                macrocell.InsertUser,
                macrocell.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_MACROCELL),
                macrocell.UpdateUser);
        }

        public static string getQueryUpdateMacroCell(MacroCell macrocell)
        {

            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}',{7}='{8}',{9}='{10}' WHERE {11}='{12}'",
                 ID_TABLE_MACROCELL,
                 ID_DESCRIPTION_MACROCELL, macrocell.Description,
                 ID_REGION_MACROCELL,macrocell.Region,
                 ID_CURRENCY_MACROCELL,macrocell.Currency.Id,
                 ID_UPDATETIME_DEVICE, macrocell.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_MACROCELL),
                 ID_UPDATEUSER_DEVICE,macrocell.UpdateUser,
                 ID_MACROCELID_MACROCELL,macrocell.Id
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
