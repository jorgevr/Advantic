using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using System.Data;
using System.Globalization;

namespace PostgreSqlClient.Queries
{
    public class CellQuery
    {
        #region const properties
        // CELL TABLE

        const string ID_TABLE_CELL = "middleware.cell";
        const string ID_CELLID_CELL = "cell_id";
        const string ID_MACROCELL_CELL = "macrocell_id";
        const string ID_LATITUDE_CELL = "cell_latitud";
        const string ID_LONGITUDE_CELL = "cell_longitud";
        const string ID_SURFACE_CELL = "cell_surface";
        const string ID_VOLUMEN_CELL = "cell_volumen";
        const string ID_BUILDING_SHADE_COEFICIENT_CELL = "cell_building_shade_coefficient";
        const string ID_WALL_INSULATION_THICKNESS_CELL = "cell_wall_insulation_thickness";
        const string ID_DESCRIPTION_CELL = "cell_desc";
        const string ID_INSERTTIME_DEVICE = "inserttime";
        const string ID_INSERTUSER_DEVICE = "insertuser";
        const string ID_UPDATETIME_DEVICE = "updatetime";
        const string ID_UPDATEUSER_DEVICE = "updateuser";
        
        

        const string DATETIMEFORMAT_CELL = "yyyyMMddHHmmss";
        const string DATETIMEFORMATINSERT_CELL = "dd/MM/yyyy HH:mm:ss";

        const int POSITION_CELLID_CELL = 0;
        const int POSITION_MACROCELL_CELL = 1;
        const int POSITION_LATITUDE_CELL = 2;
        const int POSITION_LONGITUDE_CELL = 3;
        const int POSITION_SURFACE_CELL = 4;
        const int POSITION_VOLUMEN_CELL = 5;
        const int POSITION_BUILDING_SHADE_COEFICIENT_CELL = 6;
        const int POSITION_WALL_INSULATION_THICKNESS_CELL = 7;
        const int POSITION_DESCRIPTION_CELL = 8;
        const int POSITION_INSERTTIME_MACROCELL = 9;
        const int POSITION_INSERTUSER_MACROCELL = 10;
        const int POSITION_UPDATETIME_MACROCELL = 11;
        const int POSITION_UPDATEUSER_MACROCELL = 12;

        #endregion

        public static IList<Cell> ParseDataSetToCell(DataTable dataTable)
        {
            IList<Cell> cellList = new List<Cell>();
            foreach (DataRow row in dataTable.Rows)
            {
                Cell cell = new Cell()
                {
                    Id = row[POSITION_CELLID_CELL].ToString(),
                    Macrocell=new MacroCell(row[POSITION_MACROCELL_CELL].ToString()),
                    Latitude = Double.Parse(row[POSITION_LATITUDE_CELL].ToString()),
                    Longitude = Double.Parse(row[POSITION_LONGITUDE_CELL].ToString()),
                    Surface=Double.Parse(row[POSITION_SURFACE_CELL].ToString()),
                    Volumen = Double.Parse(row[POSITION_VOLUMEN_CELL].ToString()),
                    BuildingShadeCoefficient = int.Parse(row[POSITION_BUILDING_SHADE_COEFICIENT_CELL].ToString()),
                    WallInsulationThickness = int.Parse(row[POSITION_WALL_INSULATION_THICKNESS_CELL].ToString()),
                    Description = row[POSITION_DESCRIPTION_CELL].ToString(),
                    LocalInsertTime = getDateTime(row[POSITION_INSERTTIME_MACROCELL].ToString(), DATETIMEFORMATINSERT_CELL),
                    InsertUser = row[POSITION_INSERTUSER_MACROCELL].ToString(),
                    UpdateLocalDateTime = getDateTime(row[POSITION_UPDATETIME_MACROCELL].ToString(), DATETIMEFORMATINSERT_CELL),
                    UpdateUser = row[POSITION_UPDATEUSER_MACROCELL].ToString()
                };
                cellList.Add(cell);
            }

            return cellList;
        }

        public static string getQueryForGetCell(String cellId)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}='{2}'", ID_TABLE_CELL, ID_CELLID_CELL, cellId);
        }

        public static string getQueryForGetAllCell()
        {
            return string.Format("SELECT * FROM {0}", ID_TABLE_CELL);
        }

        public static string getQuerySaveCell(Cell cell)
        {
            return string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", ID_TABLE_CELL, 
                cell.Id,
                cell.Macrocell.Id,
                cell.Latitude,
                cell.Longitude,
                cell.Surface,
                cell.Volumen,
                cell.BuildingShadeCoefficient,
                cell.WallInsulationThickness,
                cell.Description,
                cell.LocalInsertTime.ToString(DATETIMEFORMATINSERT_CELL),
                cell.InsertUser,
                cell.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_CELL),
                cell.UpdateUser);
        }

        public static string getQueryUpdateCell(Cell cell)
        {

            return string.Format("UPDATE {0} SET {1}='{2}', {3}='{4}',{5}='{6}',{7}='{8}',{9}='{10}',{11}='{12}',{13}='{14}',{15}='{16}',{17}='{18}',{19}='{20}' WHERE {21}='{22}'",
                 ID_TABLE_CELL,
                 ID_MACROCELL_CELL,cell.Macrocell.Id,
                 ID_LATITUDE_CELL, cell.Latitude,
                 ID_LONGITUDE_CELL,cell.Longitude,
                 ID_SURFACE_CELL,cell.Surface,
                 ID_VOLUMEN_CELL,cell.Volumen,
                 ID_BUILDING_SHADE_COEFICIENT_CELL,cell.BuildingShadeCoefficient,
                 ID_WALL_INSULATION_THICKNESS_CELL,cell.WallInsulationThickness,
                 ID_DESCRIPTION_CELL,cell.Description,
                 ID_UPDATETIME_DEVICE,cell.UpdateLocalDateTime.ToString(DATETIMEFORMATINSERT_CELL),
                 ID_UPDATEUSER_DEVICE,cell.UpdateUser,
                 ID_CELLID_CELL,cell.Id
                );
        }

        private static double TryParseDouble(string toParseString)
        {
            if (toParseString != String.Empty)
                return Double.Parse(toParseString);
            return 0;
        }

        private static DateTime getDateTime(string dateTimeString, string format)
        {
            DateTime result;
            DateTime.TryParseExact(dateTimeString, format, new CultureInfo("es-ES"), DateTimeStyles.None, out result);
            return result;
        }


        
    }
}
