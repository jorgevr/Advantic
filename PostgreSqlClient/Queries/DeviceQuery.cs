using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class DeviceQuery
    {
        #region const properties
        // DEVICE TABLE

        const string ID_TABLE_DEVICE = "middleware.device";
        const string ID_DEVICEID_DEVICE = "device_id";
        const string ID_DESCRIPTION_DEVICE = "device_desc";
        const string ID_APPLIANCEID_DEVICE = "appliance_id";
        const string ID_MANUFACTURERID_DEVICE = "manufacturer_id";
        const string ID_INSERTTIME_DEVICE = "inserttime";
        const string ID_INSERTUSER_DEVICE = "insertuser";
        const string ID_UPDATETIME_DEVICE = "updatetime";
        const string ID_UPDATEUSER_DEVICE = "updateuser";
        const string ID_OUTPUT_DEVICE = "device_output";
        const string ID_GATEWAY_DEVICE = "gateway_id";

        const string DATETIMEFORMAT_DEVICE = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_DEVICE = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_DEVICEID_DEVICE = 0;
        const int POSITION_DESCRIPTION_DEVICE = 1;
        const int POSITION_APPLIANCEID_DEVICE = 2;
        const int POSITION_MANUFACTURERID_DEVICE = 3;
        const int POSITION_INSERTTIME_DEVICE = 4;
        const int POSITION_INSERTUSER_DEVICE = 5;
        const int POSITION_UPDATETIME_DEVICE = 6;
        const int POSITION_UPDATEUSER_DEVICE = 7;
        const int POSITION_OUTPUT_DEVICE = 8;
        const int POSITION_GATEWAY_DEVICE = 9;


        #endregion

        public static IList<Device> ParseDataSetToDevice(DataTable dataTable)
        {
            IList<Device> deviceList = new List<Device>();
            foreach (DataRow row in dataTable.Rows)
            {
                Device device = new Device()
                {
                    Id = row[POSITION_DEVICEID_DEVICE].ToString(),
                    Description = row[POSITION_DESCRIPTION_DEVICE].ToString(),
                    Appliance = new Appliance(row[POSITION_APPLIANCEID_DEVICE].ToString()),
                    Manufacturer = new Manufacturer(row[POSITION_MANUFACTURERID_DEVICE].ToString()),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_DEVICE].ToString(), DATETIMEFORMATINSERT_DEVICE),
                    InsertUser = row[POSITION_INSERTUSER_DEVICE].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_DEVICE].ToString(), DATETIMEFORMATINSERT_DEVICE),
                    UpdateUser = row[POSITION_UPDATEUSER_DEVICE].ToString(),
                    OutPut = TryParseBoolean(row[POSITION_OUTPUT_DEVICE].ToString()),
                    Gateway = new Gateway(row[POSITION_GATEWAY_DEVICE].ToString())
                };
                deviceList.Add(device);
            }

            return deviceList;
        }

        public static string getQueryForGetDevice(String deviceId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_DEVICE, ID_DEVICEID_DEVICE, deviceId);
        }

        public static string getQueryForGetAllDevice()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_DEVICE);
        }

        public static string getQuerySaveDevice(Device device)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", ID_TABLE_DEVICE, 
                device.Id,
                device.Description,
                device.Appliance.Id,
                device.Manufacturer.Id,
                device.LocalInsertTime.ToString(DATETIMEFORMATINSERT_DEVICE),
                device.InsertUser,
                device.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_DEVICE),
                device.UpdateUser,
                device.OutPut.ToString(),
                device.Gateway.Id);
        }

        public static string getQueryUpdateDevice(Device device)
        {

            return string.Format("UPDATE {0} SET {1}='{2}',{3}='{4}',{5}='{6}',{7}='{8}',{9}='{10}',{11}='{12}',{13}='{14}' WHERE {15}='{16}'",
                 ID_TABLE_DEVICE,
                 ID_DESCRIPTION_DEVICE, device.Description,
                 ID_APPLIANCEID_DEVICE,device.Appliance.Id,
                 ID_MANUFACTURERID_DEVICE,device.Manufacturer.Id,
                 ID_UPDATETIME_DEVICE, device.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_DEVICE),
                 ID_UPDATEUSER_DEVICE,device.UpdateUser,
                 ID_OUTPUT_DEVICE,device.OutPut.ToString(),
                 ID_GATEWAY_DEVICE,device.Gateway.Id,
                 ID_DEVICEID_DEVICE,device.Id
                );
        }

        private static Boolean TryParseBoolean(string toParseString)
        {
            if (toParseString != String.Empty)
                return Boolean.Parse(toParseString);
            return false;
        }

        private static DateTime getDateTime(string dateTimeString, string format)
        {
            DateTime result;
            DateTime.TryParseExact(dateTimeString, format, new CultureInfo("es-ES"), DateTimeStyles.None, out result);
            return result;
        }


        
    }
}
