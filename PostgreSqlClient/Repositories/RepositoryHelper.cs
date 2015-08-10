using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;
using Npgsql;
using PostgreSqlClient.ConnectionProvider;
using System.Data;
using System.Globalization;
using PostgreSqlClient.Queries;


namespace PostgreSqlClient.Repositories
{
    public class RepositoryHelper
    {
        private const String SELECT_TYPE = "SELECT";
        private const String INSERT_TYPE = "INSERT";
        private const String UPDATE_TYPE = "UPDATE";

        private PostgreSqlConnectionProvider _provider;
        
        public RepositoryHelper(PostgreSqlConnectionProvider provider)
        {
            _provider = provider;
        }

        # region Public Methods

        public Measure GetMeasure(string deviceId, string typeId, DateTime localDatetime)
        {
            string query = MeasureQuery.getQueryForGetMeasure(deviceId, typeId, localDatetime);
            return MeasureQuery.ParseDataSetToMeasure(_provider.queryExecute(query, SELECT_TYPE)).First();    
        }

        public IList<Measure> GetAllMeasure()
        {
            string query = MeasureQuery.getQueryForGetAllMeasure();
            return MeasureQuery.ParseDataSetToMeasure(_provider.queryExecute(query, SELECT_TYPE));
        }

        public IList<Measure> GetByDeviceTypeAndDateTimeRange(string deviceId, string typeId, DateTime startLocalDateTime, DateTime endLocalDateTime)
        {
            string query = MeasureQuery.getQueryByDeviceTypeAndDateTimeRange(deviceId, typeId, startLocalDateTime, endLocalDateTime);
            return MeasureQuery.ParseDataSetToMeasure(_provider.queryExecute(query,SELECT_TYPE));
        }

        public void SaveMeasure(Measure measure)
        {
            string query = MeasureQuery.getQuerySaveMeasure(measure);
            MeasureQuery.ParseDataSetToMeasure(_provider.queryExecute(query,INSERT_TYPE));
        }

        public void SaveMeasureList(IList<Measure> measureList)
        {
            foreach (Measure measure in measureList) 
            {
                SaveMeasure(measure);
            }
        }

        public void UpdateMeasure(Measure measure)
        {
            string query = MeasureQuery.getQueryUpdateMeasure(measure);
            MeasureQuery.ParseDataSetToMeasure(_provider.queryExecute(query,UPDATE_TYPE));
        }

        public Weather GetWeather(string macrocellId, string typeId, DateTime localDatetime)
        {
            string query = WeatherQuery.getQueryForGetWeather(macrocellId, typeId, localDatetime);
            return WeatherQuery.ParseDataSetToWeather(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        public IList<Weather> GetAllWeather()
        {
            string query = WeatherQuery.getQueryForGetAllWeather();
            return WeatherQuery.ParseDataSetToWeather(_provider.queryExecute(query,SELECT_TYPE));
        }

        public IList<Weather> GetBymacroCellTypeAndDateTimeRange(string macrocellId, string typeId, DateTime startLocalDateTime, DateTime endLocalDateTime)
        {
            string query = WeatherQuery.getQueryByMacrocellTypeAndDateTimeRange(macrocellId, typeId, startLocalDateTime, endLocalDateTime);
            return WeatherQuery.ParseDataSetToWeather(_provider.queryExecute(query,SELECT_TYPE));
        }

        public void SaveWeather(Weather weather)
        {
            string query = WeatherQuery.getQuerySaveWeather(weather);
            WeatherQuery.ParseDataSetToWeather(_provider.queryExecute(query,INSERT_TYPE));
        }

        public void SaveWeatherList(IList<Weather> weatherList)
        {
            foreach (Weather weather in weatherList)
            {
                SaveWeather(weather);
            }
        }

        public void UpdateWeather(Weather weather)
        {
            string query = WeatherQuery.getQueryUpdateWeather(weather);
            WeatherQuery.ParseDataSetToWeather(_provider.queryExecute(query,UPDATE_TYPE));
        }


        public Device GetDevice(string deviceId)
        {
            string query = DeviceQuery.getQueryForGetDevice(deviceId);
            return DeviceQuery.ParseDataSetToDevice(_provider.queryExecute(query,SELECT_TYPE)).First();  
        }

        public IList<Device> GetAllDevice()
        {
            string query = DeviceQuery.getQueryForGetAllDevice();
            return DeviceQuery.ParseDataSetToDevice(_provider.queryExecute(query,SELECT_TYPE));
        }

        public void SaveDevice(Device device)
        {
            string query = DeviceQuery.getQuerySaveDevice(device);
            DeviceQuery.ParseDataSetToDevice(_provider.queryExecute(query,INSERT_TYPE));
        }

        public void SaveDeviceList(IList<Device> deviceList)
        {
            foreach (Device device in deviceList)
            {
                SaveDevice(device);
            }
        }

        public void UpdateDevice(Device device)
        {
            string query = DeviceQuery.getQueryUpdateDevice(device);
            DeviceQuery.ParseDataSetToDevice(_provider.queryExecute(query,UPDATE_TYPE));
        }

        public MeasureType GetMeasureType(string measureTypeId)
        {
            string query = MeasureTypeQuery.getQueryForGetMeasureType(measureTypeId);
            return MeasureTypeQuery.ParseDataSetToMeasureType(_provider.queryExecute(query,SELECT_TYPE)).First(); 
        }

        public IList<MeasureType> GetAllMeasureType()
        {
            string query = MeasureTypeQuery.getQueryForGetAllMeasureType();
            return MeasureTypeQuery.ParseDataSetToMeasureType(_provider.queryExecute(query,SELECT_TYPE));
        }

        public void SaveMeasureType(MeasureType measureType)
        {
            string query = MeasureTypeQuery.getQuerySaveMeasureType(measureType);
            MeasureTypeQuery.ParseDataSetToMeasureType(_provider.queryExecute(query,INSERT_TYPE));
        }

        public void SaveMeasureTypeList(IList<MeasureType> measureTypeList)
        {
            foreach (MeasureType measureType in measureTypeList)
            {
                SaveMeasureType(measureType);
            }
        }

        public void UpdateMeasureType(MeasureType measureType)
        {
            string query = MeasureTypeQuery.getQueryUpdateMeasureType(measureType);
            MeasureTypeQuery.ParseDataSetToMeasureType(_provider.queryExecute(query,UPDATE_TYPE));
        }

        internal UnitType GetUnitType(string unitTypeId)
        {
            string query = UnitTypeQuery.getQueryForGetUnitType(unitTypeId);
            return UnitTypeQuery.ParseDataSetToUnitType(_provider.queryExecute(query,SELECT_TYPE)).First(); 
        }

        internal IList<UnitType> GetAllUnitType()
        {
            string query = UnitTypeQuery.getQueryForGetAllUnitType();
            return UnitTypeQuery.ParseDataSetToUnitType(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveUnitType(UnitType unitType)
        {
            string query = UnitTypeQuery.getQuerySaveUnitType(unitType);
            UnitTypeQuery.ParseDataSetToUnitType(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveUnitTypeList(IList<UnitType> unitTypeList)
        {
            foreach (UnitType unitType in unitTypeList)
            {
                SaveUnitType(unitType);
            }
        }

        internal void UpdateUnitType(UnitType unitType)
        {
            string query = UnitTypeQuery.getQueryUpdateUnitType(unitType);
            UnitTypeQuery.ParseDataSetToUnitType(_provider.queryExecute(query,UPDATE_TYPE));
        }

        internal MacroCell GetMacroCell(string macrocellId)
        {
            string query = MacroCellQuery.getQueryForGetMacroCell(macrocellId);
            return MacroCellQuery.ParseDataSetToMacroCell(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<MacroCell> GetAllMacroCell()
        {
            string query = MacroCellQuery.getQueryForGetAllMacroCell();
            return MacroCellQuery.ParseDataSetToMacroCell(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveMacroCell(MacroCell macrocell)
        {
            string query = MacroCellQuery.getQuerySaveMacroCell(macrocell);
            MacroCellQuery.ParseDataSetToMacroCell(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveMacroCellList(IList<MacroCell> macrocellList)
        {
            foreach (MacroCell macrocell in macrocellList)
            {
                SaveMacroCell(macrocell);
            }
        }

        internal void UpdateMacroCell(MacroCell macrocell)
        {
            string query = MacroCellQuery.getQueryUpdateMacroCell(macrocell);
            MacroCellQuery.ParseDataSetToMacroCell(_provider.queryExecute(query,UPDATE_TYPE));
        }

        internal Currency GetCurrency(string currencyId)
        {
            string query = CurrencyQuery.getQueryForGetCurrency(currencyId);
            return CurrencyQuery.ParseDataSetToCurrency(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<Currency> GetAllCurrency()
        {
            string query = CurrencyQuery.getQueryForGetAllCurrency();
            return CurrencyQuery.ParseDataSetToCurrency(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveCurrency(Currency currency)
        {
            string query = CurrencyQuery.getQuerySaveCurrency(currency);
            CurrencyQuery.ParseDataSetToCurrency(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveCurrencyList(IList<Currency> currencyList)
        {
            foreach (Currency currency in currencyList)
            {
                SaveCurrency(currency);
            }
        }

        internal void UpdateCurrency(Currency currency)
        {
            string query = CurrencyQuery.getQueryUpdateCurrency(currency);
            CurrencyQuery.ParseDataSetToCurrency(_provider.queryExecute(query,UPDATE_TYPE));
        }

        internal Cell GetCell(string cellId)
        {
            string query = CellQuery.getQueryForGetCell(cellId);
            return CellQuery.ParseDataSetToCell(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<Cell> GetAllCell()
        {
            string query = CellQuery.getQueryForGetAllCell();
            return CellQuery.ParseDataSetToCell(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveCell(Cell cell)
        {
            string query = CellQuery.getQuerySaveCell(cell);
            CellQuery.ParseDataSetToCell(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveCellList(IList<Cell> cellList)
        {
            foreach (Cell cell in cellList)
            {
                SaveCell(cell);
            }
        }

        internal void UpdateCell(Cell cell)
        {
            string query = CellQuery.getQueryUpdateCell(cell);
            CellQuery.ParseDataSetToCell(_provider.queryExecute(query,UPDATE_TYPE));
        }


        internal Room GetRoom(string roomId)
        {
            string query = RoomQuery.getQueryForGetRoom(roomId);
            return RoomQuery.ParseDataSetToRoom(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<Room> GetAllRoom()
        {
            string query = RoomQuery.getQueryForGetAllRoom();
            return RoomQuery.ParseDataSetToRoom(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveRoom(Room room)
        {
            string query = RoomQuery.getQuerySaveRoom(room);
            RoomQuery.ParseDataSetToRoom(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveRoomList(IList<Room> roomList)
        {
            foreach (Room room in roomList)
            {
                SaveRoom(room);
            }
        }

        internal void UpdateRoom(Room room)
        {
            string query = RoomQuery.getQueryUpdateRoom(room);
            RoomQuery.ParseDataSetToRoom(_provider.queryExecute(query,UPDATE_TYPE));
        }

        internal Appliance GetAppliance(string applianceId)
        {
            string query = ApplianceQuery.getQueryForGetAppliance(applianceId);
            return ApplianceQuery.ParseDataSetToAppliance(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<Appliance> GetAllAppliance()
        {
            string query = ApplianceQuery.getQueryForGetAllAppliance();
            return ApplianceQuery.ParseDataSetToAppliance(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveAppliance(Appliance appliance)
        {
            string query = ApplianceQuery.getQuerySaveAppliance(appliance);
            ApplianceQuery.ParseDataSetToAppliance(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveApplianceList(IList<Appliance> applianceList)
        {
            foreach (Appliance appliance in applianceList)
            {
                SaveAppliance(appliance);
            }
        }

        internal void UpdateAppliance(Appliance appliance)
        {
            string query = ApplianceQuery.getQueryUpdateAppliance(appliance);
            ApplianceQuery.ParseDataSetToAppliance(_provider.queryExecute(query,UPDATE_TYPE));
        }

        internal Category GetCategory(string categoryId)
        {
            string query = CategoryQuery.getQueryForGetCategory(categoryId);
            return CategoryQuery.ParseDataSetToCategory(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<Category> GetAllCategory()
        {
            string query = CategoryQuery.getQueryForGetAllCategory();
            return CategoryQuery.ParseDataSetToCategory(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveCategory(Category category)
        {
            string query = CategoryQuery.getQuerySaveCategory(category);
            CategoryQuery.ParseDataSetToCategory(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveCategoryList(IList<Category> categoryList)
        {
            foreach (Category category in categoryList)
            {
                SaveCategory(category);
            }
        }

        internal void UpdateCategory(Category category)
        {
            string query = CategoryQuery.getQueryUpdateCategory(category);
            CategoryQuery.ParseDataSetToCategory(_provider.queryExecute(query,UPDATE_TYPE));
        }

        internal Gateway GetGateway(string gatewayId)
        {
            string query = GatewayQuery.getQueryForGetGateway(gatewayId);
            return GatewayQuery.ParseDataSetToGateway(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<Gateway> GetAllGateway()
        {
            string query = GatewayQuery.getQueryForGetAllGateway();
            return GatewayQuery.ParseDataSetToGateway(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveGateway(Gateway gateway)
        {
            string query = GatewayQuery.getQuerySaveGateway(gateway);
            GatewayQuery.ParseDataSetToGateway(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveGatewayList(IList<Gateway> gatewayList)
        {
            foreach (Gateway gateway in gatewayList)
            {
                SaveGateway(gateway);
            }
        }

        internal void UpdateGateway(Gateway gateway)
        {
            string query = GatewayQuery.getQueryUpdateGateway(gateway);
            GatewayQuery.ParseDataSetToGateway(_provider.queryExecute(query,UPDATE_TYPE));
        }


        internal LoadType GetLoadType(string loadTypeId)
        {
            string query = LoadTypeQuery.getQueryForGetLoadType(loadTypeId);
            return LoadTypeQuery.ParseDataSetToLoadType(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<LoadType> GetAllLoadType()
        {
            string query = LoadTypeQuery.getQueryForGetAllLoadType();
            return LoadTypeQuery.ParseDataSetToLoadType(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveLoadType(LoadType loadType)
        {
            string query = LoadTypeQuery.getQuerySaveLoadType(loadType);
            LoadTypeQuery.ParseDataSetToLoadType(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveLoadTypeList(IList<LoadType> loadTypeList)
        {
            foreach (LoadType loadType in loadTypeList)
            {
                SaveLoadType(loadType);
            }
        }

        internal void UpdateLoadType(LoadType loadType)
        {
            string query = LoadTypeQuery.getQueryUpdateLoadType(loadType);
            LoadTypeQuery.ParseDataSetToLoadType(_provider.queryExecute(query,UPDATE_TYPE));
        }


        internal Manufacturer GetManufacturer(string manufacturerId)
        {
            string query = ManufacturerQuery.getQueryForGetManufacturer(manufacturerId);
            return ManufacturerQuery.ParseDataSetToManufacturer(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<Manufacturer> GetAllManufacturer()
        {
            string query = ManufacturerQuery.getQueryForGetAllManufacturer();
            return ManufacturerQuery.ParseDataSetToManufacturer(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveManufacturer(Manufacturer manufacturer)
        {
            string query = ManufacturerQuery.getQuerySaveManufacturer(manufacturer);
            ManufacturerQuery.ParseDataSetToManufacturer(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveManufacturerList(IList<Manufacturer> manufacturerList)
        {
            foreach (Manufacturer manufacturer in manufacturerList)
            {
                SaveManufacturer(manufacturer);
            }
        }

        internal void UpdateManufacturer(Manufacturer manufacturer)
        {
            string query = ManufacturerQuery.getQueryUpdateManufacturer(manufacturer);
            ManufacturerQuery.ParseDataSetToManufacturer(_provider.queryExecute(query,UPDATE_TYPE));
        }

        internal SubCategory GetSubCategory(string subcategoryId)
        {
            string query = SubCategoryQuery.getQueryForGetSubCategory(subcategoryId);
            return SubCategoryQuery.ParseDataSetToSubCategory(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<SubCategory> GetAllSubCategory()
        {
            string query = SubCategoryQuery.getQueryForGetAllSubCategory();
            return SubCategoryQuery.ParseDataSetToSubCategory(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveSubCategory(SubCategory subcategory)
        {
            string query = SubCategoryQuery.getQuerySaveSubCategory(subcategory);
            SubCategoryQuery.ParseDataSetToSubCategory(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveSubCategoryList(IList<SubCategory> subcategoryList)
        {
            foreach (SubCategory subcategory in subcategoryList)
            {
                SaveSubCategory(subcategory);
            }
        }

        internal void UpdateSubCategory(SubCategory subcategory)
        {
            string query = SubCategoryQuery.getQueryUpdateSubCategory(subcategory);
            SubCategoryQuery.ParseDataSetToSubCategory(_provider.queryExecute(query,UPDATE_TYPE));
        }

        internal SuperCategory GetSuperCategory(string supercategoryId)
        {
            string query = SuperCategoryQuery.getQueryForGetSuperCategory(supercategoryId);
            return SuperCategoryQuery.ParseDataSetToSuperCategory(_provider.queryExecute(query,SELECT_TYPE)).First();
        }

        internal IList<SuperCategory> GetAllSuperCategory()
        {
            string query = SuperCategoryQuery.getQueryForGetAllSuperCategory();
            return SuperCategoryQuery.ParseDataSetToSuperCategory(_provider.queryExecute(query,SELECT_TYPE));
        }

        internal void SaveSuperCategory(SuperCategory supercategory)
        {
            string query = SuperCategoryQuery.getQuerySaveSuperCategory(supercategory);
            SuperCategoryQuery.ParseDataSetToSuperCategory(_provider.queryExecute(query,INSERT_TYPE));
        }

        internal void SaveSuperCategoryList(IList<SuperCategory> supercategoryList)
        {
            foreach (SuperCategory supercategory in supercategoryList)
            {
                SaveSuperCategory(supercategory);
            }
        }

        internal void UpdateSuperCategory(SuperCategory supercategory)
        {
            string query = SuperCategoryQuery.getQueryUpdateSuperCategory(supercategory);
            SuperCategoryQuery.ParseDataSetToSuperCategory(_provider.queryExecute(query,UPDATE_TYPE));
        }


        #endregion













        
    }
}
