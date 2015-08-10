using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class RoomQuery
    {
        #region const properties
        // UnitType TABLE

        const string ID_TABLE_ROOM = "middleware.room";
        const string ID_ID_ROOM = "room_id";
        const string ID_CELL_ROOM = "cell_id";
        const string ID_SURFACE_ROOM = "room_surface";
        const string ID_VOLUMEN_ROOM = "room_volume";
        const string ID_MAX_OCUP_ROOM = "room_max_ocup";
        const string ID_DESCRIPTION_ROOM = "unit_type_desc";
        const string ID_INSERTTIME_ROOM = "inserttime";
        const string ID_INSERTUSER_ROOM = "insertuser";
        const string ID_UPDATETIME_ROOM = "updatetime";
        const string ID_UPDATEUSER_ROOM = "updateuser";

        const string DATETIMEFORMAT_ROOM = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_ROOM = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_ID_ROOM = 0;
        const int POSITION_CELL_ROOM = 1;
        const int POSITION_SURFACE_ROOM = 2;
        const int POSITION_VOLUMEN_ROOM = 3;
        const int POSITION_MAX_OCUP_ROOM = 4;
        const int POSITION_DESCRIPTION_ROOM = 5;
        const int POSITION_INSERTTIME_ROOM = 6;
        const int POSITION_INSERTUSER_ROOM = 7;
        const int POSITION_UPDATETIME_ROOM = 8;
        const int POSITION_UPDATEUSER_ROOM = 9;

        #endregion

        public static IList<Room> ParseDataSetToRoom(DataTable dataTable)
        {
            IList<Room> roomList = new List<Room>();
            foreach (DataRow row in dataTable.Rows)
            {
                Room room = new Room()
                {
                    Id = row[POSITION_ID_ROOM].ToString(),
                    Cell=new Cell(row[POSITION_CELL_ROOM].ToString()),
                    Surface=Double.Parse(row[POSITION_SURFACE_ROOM].ToString()),
                    Volumen = Double.Parse(row[POSITION_VOLUMEN_ROOM].ToString()),
                    MaxOcup = int.Parse(row[POSITION_MAX_OCUP_ROOM].ToString()),
                    Description = row[POSITION_DESCRIPTION_ROOM].ToString(),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_ROOM].ToString(), DATETIMEFORMATINSERT_ROOM),
                    InsertUser = row[POSITION_INSERTUSER_ROOM].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_ROOM].ToString(),DATETIMEFORMATINSERT_ROOM),
                    UpdateUser = row[POSITION_UPDATEUSER_ROOM].ToString()
                };
                roomList.Add(room);
            }

            return roomList;
        }

        public static string getQueryForGetRoom(string roomId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_ROOM, ID_ID_ROOM, roomId);
        }

        public static string getQueryForGetAllRoom()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_ROOM);
        }

        public static string getQuerySaveRoom(Room room)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", ID_TABLE_ROOM,
                room.Id,
                room.Cell.Id,
                room.Surface,
                room.Volumen,
                room.MaxOcup,
                room.Description,
                room.LocalInsertTime.ToString(DATETIMEFORMATINSERT_ROOM),
                room.InsertUser,
                room.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_ROOM),
                room.UpdateUser);
        }

        public static string getQueryUpdateRoom(Room room)
        {
            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}' ,{7}='{8}' ,{9}='{10}',{11}='{12}',{13}='{14}' WHERE {15}='{16}'",
                 ID_TABLE_ROOM,
                 ID_CELL_ROOM,room.Cell.Id,
                 ID_SURFACE_ROOM,room.Surface,
                 ID_VOLUMEN_ROOM,room.Volumen,
                 ID_MAX_OCUP_ROOM,room.MaxOcup,
                 ID_DESCRIPTION_ROOM, room.Description,
                 ID_UPDATETIME_ROOM, room.UpdateLocalDateTime.ToString(DATETIMEFORMAT_ROOM),
                 ID_UPDATEUSER_ROOM, room.UpdateUser,
                 ID_ID_ROOM, room.Id
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
