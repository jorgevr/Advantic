using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class UnitTypeQuery
    {
        #region const properties
        // UnitType TABLE

        const string ID_TABLE_UNITTYPE = "middleware.unit_type";
        const string ID_ID_UNITTYPE = "unit_type_id";
        const string ID_DESCRIPTION_UNITTYPE = "unit_type_desc";
        const string ID_INSERTTIME_UNITTYPE = "inserttime";
        const string ID_INSERTUSER_UNITTYPE = "insertuser";
        const string ID_UPDATETIME_UNITTYPE = "updatetime";
        const string ID_UPDATEUSER_UNITTYPE = "updateuser";

        const string DATETIMEFORMAT_UNITTYPE = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_UNITTYPE = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_ID_UNITTYPE = 0;
        const int POSITION_DESCRIPTION_UNITTYPE = 1;
        const int POSITION_INSERTTIME_UNITTYPE = 2;
        const int POSITION_INSERTUSER_UNITTYPE = 3;
        const int POSITION_UPDATETIME_UNITTYPE = 4;
        const int POSITION_UPDATEUSER_UNITTYPE = 5;

        #endregion

        public static IList<UnitType> ParseDataSetToUnitType(DataTable dataTable)
        {
            IList<UnitType> unitTypeList = new List<UnitType>();
            foreach (DataRow row in dataTable.Rows)
            {
                UnitType unitType = new UnitType()
                {
                    Id = row[POSITION_ID_UNITTYPE].ToString(),
                    Description = row[POSITION_DESCRIPTION_UNITTYPE].ToString(),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_UNITTYPE].ToString(), DATETIMEFORMATINSERT_UNITTYPE),
                    InsertUser = row[POSITION_INSERTUSER_UNITTYPE].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_UNITTYPE].ToString(), DATETIMEFORMATINSERT_UNITTYPE),
                    UpdateUser = row[POSITION_UPDATEUSER_UNITTYPE].ToString()
                };
                unitTypeList.Add(unitType);
            }

            return unitTypeList;
        }

        public static string getQueryForGetUnitType(string unitTypeId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_UNITTYPE, ID_ID_UNITTYPE, unitTypeId);
        }

        public static string getQueryForGetAllUnitType()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_UNITTYPE);
        }

        public static string getQuerySaveUnitType(UnitType unitType)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}')", ID_TABLE_UNITTYPE,
                unitType.Id,
                unitType.Description,
                unitType.LocalInsertTime.ToString(DATETIMEFORMATINSERT_UNITTYPE),
                unitType.InsertUser,
                unitType.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_UNITTYPE),
                unitType.UpdateUser);
        }

        public static string getQueryUpdateUnitType(UnitType unitType)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}'WHERE {7}='{8}'",
                 ID_TABLE_UNITTYPE,
                 ID_DESCRIPTION_UNITTYPE, unitType.Description,
                 ID_UPDATETIME_UNITTYPE, unitType.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_UNITTYPE),
                 ID_UPDATEUSER_UNITTYPE, unitType.UpdateUser,
                 ID_ID_UNITTYPE, unitType.Id
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
