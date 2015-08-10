using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class ManufacturerQuery
    {
        #region const properties
        // Manufacturer TABLE

        const string ID_TABLE_MANUFACTURER = "middleware.manufacturer";
        const string ID_ID_MANUFACTURER = "manufacturer_id";
        const string ID_MAKE_MANUFACTURER = "manufacturer_make";
        const string ID_MODEL_MANUFACTURER = "manufacturer_model";
        const string ID_FIRMWARE_MANUFACTURER = "manufacturer_firmware";
        const string ID_DESCRIPTION_MANUFACTURER = "manufacturer_desc";
        const string ID_INSERTTIME_MANUFACTURER = "inserttime";
        const string ID_INSERTUSER_MANUFACTURER = "insertuser";
        const string ID_UPDATETIME_MANUFACTURER = "updatetime";
        const string ID_UPDATEUSER_MANUFACTURER = "updateuser";

        const string DATETIMEFORMAT_MANUFACTURER = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_MANUFACTURER = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_ID_MANUFACTURER = 0;
        const int POSITION_MAKE_MANUFACTURER = 1;
        const int POSITION_MODEL_MANUFACTURER = 2;
        const int POSITION_FIRMWARE_MANUFACTURER = 3;
        const int POSITION_DESCRIPTION_MANUFACTURER = 4;
        const int POSITION_INSERTTIME_MANUFACTURER = 5;
        const int POSITION_INSERTUSER_MANUFACTURER = 6;
        const int POSITION_UPDATETIME_MANUFACTURER = 7;
        const int POSITION_UPDATEUSER_MANUFACTURER = 8;

        #endregion

        public static IList<Manufacturer> ParseDataSetToManufacturer(DataTable dataTable)
        {
            IList<Manufacturer> manufacturerList = new List<Manufacturer>();
            foreach (DataRow row in dataTable.Rows)
            {
                Manufacturer manufacturer = new Manufacturer()
                {
                    Id = row[POSITION_ID_MANUFACTURER].ToString(),
                    Make = row[POSITION_MAKE_MANUFACTURER].ToString(),
                    Model = row[POSITION_MODEL_MANUFACTURER].ToString(),
                    Firmware = row[POSITION_FIRMWARE_MANUFACTURER].ToString(),
                    Description = row[POSITION_DESCRIPTION_MANUFACTURER].ToString(),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_MANUFACTURER].ToString(), DATETIMEFORMATINSERT_MANUFACTURER),
                    InsertUser = row[POSITION_INSERTUSER_MANUFACTURER].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_MANUFACTURER].ToString(), DATETIMEFORMATINSERT_MANUFACTURER),
                    UpdateUser = row[POSITION_UPDATEUSER_MANUFACTURER].ToString()
                };
                manufacturerList.Add(manufacturer);
            }

            return manufacturerList;
        }

        public static string getQueryForGetManufacturer(string manufacturerId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_MANUFACTURER, ID_ID_MANUFACTURER, manufacturerId);
        }

        public static string getQueryForGetAllManufacturer()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_MANUFACTURER);
        }

        public static string getQuerySaveManufacturer(Manufacturer manufacturer)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ID_TABLE_MANUFACTURER,
                manufacturer.Id,
                manufacturer.Make,
                manufacturer.Model,
                manufacturer.Firmware,
                manufacturer.Description,
                manufacturer.LocalInsertTime.ToString(DATETIMEFORMATINSERT_MANUFACTURER),
                manufacturer.InsertUser,
                manufacturer.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_MANUFACTURER),
                manufacturer.UpdateUser);
        }

        public static string getQueryUpdateManufacturer(Manufacturer manufacturer)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}',{7}='{8}',{9}='{10}',{11}='{12}' WHERE {13}='{14}'",
                 ID_TABLE_MANUFACTURER,
                 ID_MAKE_MANUFACTURER,manufacturer.Make,
                 ID_MODEL_MANUFACTURER,manufacturer.Model,
                 ID_FIRMWARE_MANUFACTURER,manufacturer.Firmware,
                 ID_DESCRIPTION_MANUFACTURER, manufacturer.Description,
                 ID_UPDATETIME_MANUFACTURER, manufacturer.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_MANUFACTURER),
                 ID_UPDATEUSER_MANUFACTURER, manufacturer.UpdateUser,
                 ID_ID_MANUFACTURER, manufacturer.Id
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
