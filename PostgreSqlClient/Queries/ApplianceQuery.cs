using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class ApplianceQuery
    {
        #region const properties
        // Appliance TABLE

        const string ID_TABLE_APPLIANCE = "middleware.appliance";
        const string ID_ID_APPLIANCE = "appliance_id";
        const string ID_POWER_APPLIANCE = "appliance_power";
        const string ID_SUBCATEGORY_APPLIANCE = "subcategory_id";
        const string ID_DESCRIPTION_APPLIANCE = "appliance_desc";
        const string ID_INVESTMENT_APPLIANCE = "appliance_investment";
        const string ID_ROOM_APPLIANCE = "room_id";
        const string ID_ACTIVEDATETIME_APPLIANCE = "appliance_activacion_date";
        const string ID_LEAVINGDATETIME_APPLIANCE = "appliance_leaving_date";
        const string ID_INSERTTIME_APPLIANCE = "inserttime";
        const string ID_INSERTUSER_APPLIANCE = "insertuser";
        const string ID_UPDATETIME_APPLIANCE = "updatetime";
        const string ID_UPDATEUSER_APPLIANCE = "updateuser";
        const string ID_LOADTYPE_APPLIANCE = "load_type_id";

        const string DATETIMEFORMAT_APPLIANCE = "yyyyMMdd";
        const string DATETIMEFORMATINSERT_APPLIANCE = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_ID_APPLIANCE = 0;
        const int POSITION_POWER_APPLIANCE = 1;
        const int POSITION_SUBCATEGORY_APPLIANCE = 2;
        const int POSITION_DESCRIPTION_APPLIANCE = 3;
        const int POSITION_INVESTMENT_APPLIANCE = 4;
        const int POSITION_ROOM_APPLIANCE = 5;
        const int POSITION_ACTIVATEDATETIME_APPLIANCE = 6;
        const int POSITION_LEAVINGDATETIME_APPLIANCE = 7;
        const int POSITION_INSERTTIME_APPLIANCE = 8;
        const int POSITION_INSERTUSER_APPLIANCE = 9;
        const int POSITION_UPDATETIME_APPLIANCE = 10;
        const int POSITION_UPDATEUSER_APPLIANCE = 11;
        const int POSITION_LOADTYPE_APPLIANCE = 12;

        #endregion

        public static IList<Appliance> ParseDataSetToAppliance(DataTable dataTable)
        {
            IList<Appliance> applianceList = new List<Appliance>();
            foreach (DataRow row in dataTable.Rows)
            {
                Appliance appliance = new Appliance()
                {
                    Id = row[POSITION_ID_APPLIANCE].ToString(),
                    Power = TryParseDouble(row[POSITION_POWER_APPLIANCE].ToString()),
                    Subcategory = new SubCategory(row[POSITION_SUBCATEGORY_APPLIANCE].ToString()),
                    Description = row[POSITION_DESCRIPTION_APPLIANCE].ToString(),
                    Investment = TryParseDouble(row[POSITION_INVESTMENT_APPLIANCE].ToString()),
                    Room = new Room(row[POSITION_ROOM_APPLIANCE].ToString()),
                    ActivacionDateTime = getDateTime(row[POSITION_ACTIVATEDATETIME_APPLIANCE].ToString(), DATETIMEFORMAT_APPLIANCE),
                    LeavingDateTime=getDateTime(row[POSITION_LEAVINGDATETIME_APPLIANCE].ToString(), DATETIMEFORMAT_APPLIANCE),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_APPLIANCE].ToString(),DATETIMEFORMATINSERT_APPLIANCE),
                    InsertUser = row[POSITION_INSERTUSER_APPLIANCE].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_APPLIANCE].ToString(),DATETIMEFORMATINSERT_APPLIANCE),
                    UpdateUser = row[POSITION_UPDATEUSER_APPLIANCE].ToString(),
                    Loadtype = new LoadType(row[POSITION_LOADTYPE_APPLIANCE].ToString())
                };
                applianceList.Add(appliance);
            }

            return applianceList;
        }

        private static double TryParseDouble(string toParseString)
        {
            if (toParseString != String.Empty)
                return Double.Parse(toParseString);
            return 0;
        }

        public static string getQueryForGetAppliance(string applianceId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_APPLIANCE, ID_ID_APPLIANCE, applianceId);
        }

        public static string getQueryForGetAllAppliance()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_APPLIANCE);
        }

        public static string getQuerySaveAppliance(Appliance appliance)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", ID_TABLE_APPLIANCE,
                appliance.Id,
                appliance.Power,
                appliance.Subcategory.Id,
                appliance.Description,
                appliance.Investment,
                appliance.Room.Id,
                appliance.ActivacionDateTime.ToString(DATETIMEFORMAT_APPLIANCE),
                appliance.LeavingDateTime.ToString(DATETIMEFORMAT_APPLIANCE),
                appliance.LocalInsertTime.ToString(DATETIMEFORMAT_APPLIANCE),
                appliance.InsertUser,
                appliance.UpdateLocalDateTime.ToString(DATETIMEFORMAT_APPLIANCE),
                appliance.UpdateUser,
                appliance.Loadtype.Id);
        }

        public static string getQueryUpdateAppliance(Appliance appliance)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}',{7}='{8}',{9}='{10}',{11}='{12}',{13}='{14}',{15}='{16}',{17}='{18}',{19}='{20}'WHERE {21}='{22}'",
                 ID_TABLE_APPLIANCE,
                 ID_POWER_APPLIANCE,appliance.Power,
                 ID_SUBCATEGORY_APPLIANCE,appliance.Subcategory.Id,
                 ID_DESCRIPTION_APPLIANCE,appliance.Description,
                 ID_INVESTMENT_APPLIANCE,appliance.Investment,
                 ID_ROOM_APPLIANCE,appliance.Room.Id,
                 ID_ACTIVEDATETIME_APPLIANCE, appliance.ActivacionDateTime.ToString(DATETIMEFORMAT_APPLIANCE),
                 ID_LEAVINGDATETIME_APPLIANCE, appliance.LeavingDateTime.ToString(DATETIMEFORMAT_APPLIANCE),
                 ID_UPDATETIME_APPLIANCE, appliance.UpdateLocalDateTime.ToString(DATETIMEFORMAT_APPLIANCE),
                 ID_UPDATEUSER_APPLIANCE, appliance.UpdateUser,
                 ID_LOADTYPE_APPLIANCE,appliance.Loadtype.Id,
                 ID_ID_APPLIANCE, appliance.Id
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
