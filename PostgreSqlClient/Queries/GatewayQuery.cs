using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class GatewayQuery
    {
        #region const properties
        // Gateway TABLE

        const string ID_TABLE_GATEWAY = "middleware.gateway";
        const string ID_ID_GATEWAY = "gateway_id";
        const string ID_DESCRIPTION_GATEWAY = "gateway_desc";
        const string ID_MACADR_GATEWAY = "gateway_macadr";
        const string ID_MANUFACTURER_GATEWAY = "manufacturer_id";
        const string ID_INSERTTIME_GATEWAY = "inserttime";
        const string ID_INSERTUSER_GATEWAY = "insertuser";
        const string ID_UPDATETIME_GATEWAY = "updatetime";
        const string ID_UPDATEUSER_GATEWAY = "updateuser";

        const string DATETIMEFORMAT_GATEWAY = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_GATEWAY = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_ID_GATEWAY = 0;
        const int POSITION_DESCRIPTION_GATEWAY = 1;
        const int POSITION_MACADR_GATEWAY = 2;
        const int POSITION_MANUFACTURER_GATEWAY = 3;
        const int POSITION_INSERTTIME_GATEWAY= 4;
        const int POSITION_INSERTUSER_GATEWAY = 5;
        const int POSITION_UPDATETIME_GATEWAY = 6;
        const int POSITION_UPDATEUSER_GATEWAY = 7;

        #endregion

        public static IList<Gateway> ParseDataSetToGateway(DataTable dataTable)
        {
            IList<Gateway> gatewayList = new List<Gateway>();
            foreach (DataRow row in dataTable.Rows)
            {
                Gateway gateway = new Gateway()
                {
                    Id = row[POSITION_ID_GATEWAY].ToString(),
                    Description = row[POSITION_DESCRIPTION_GATEWAY].ToString(),
                    MacAddress = int.Parse(row[POSITION_MACADR_GATEWAY].ToString()),
                    Manufacturer=new Manufacturer(row[POSITION_MACADR_GATEWAY].ToString()),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_GATEWAY].ToString(), DATETIMEFORMATINSERT_GATEWAY),
                    InsertUser = row[POSITION_INSERTUSER_GATEWAY].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_GATEWAY].ToString(), DATETIMEFORMATINSERT_GATEWAY),
                    UpdateUser = row[POSITION_UPDATEUSER_GATEWAY].ToString()
                };
                gatewayList.Add(gateway);
            }

            return gatewayList;
        }

        public static string getQueryForGetGateway(string gatewayId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_GATEWAY, ID_ID_GATEWAY, gatewayId);
        }

        public static string getQueryForGetAllGateway()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_GATEWAY);
        }

        public static string getQuerySaveGateway(Gateway gateway)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", ID_TABLE_GATEWAY,
                gateway.Id,
                gateway.Description,
                gateway.MacAddress,
                gateway.Manufacturer.Id,
                gateway.LocalInsertTime.ToString(DATETIMEFORMATINSERT_GATEWAY),
                gateway.InsertUser,
                gateway.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_GATEWAY),
                gateway.UpdateUser);
        }

        public static string getQueryUpdateGateway(Gateway gateway)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}',{7}='{8}',{9}='{10}' WHERE {11}='{12}'",
                 ID_TABLE_GATEWAY,
                 ID_DESCRIPTION_GATEWAY, gateway.Description,
                 ID_MACADR_GATEWAY,gateway.MacAddress,
                 ID_MANUFACTURER_GATEWAY,gateway.Manufacturer.Id,
                 ID_UPDATETIME_GATEWAY, gateway.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_GATEWAY),
                 ID_UPDATEUSER_GATEWAY, gateway.UpdateUser,
                 ID_ID_GATEWAY, gateway.Id
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
