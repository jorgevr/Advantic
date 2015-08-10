using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface IWeatherRepository
    {
        bool Exists(Weather weather);
        void Save(Weather weather);
        void SaveList(IList<Weather> weatherList);
        void Update(Weather weather);
        Weather Get(String macrocellId, String typeId, DateTime localDatetime);
        IList<Weather> GetByMacrocellTypeAndDateTimeRange(String macrocell, String measureType, DateTime startLocalDateTime, DateTime endLocalDateTime);
        IList<Weather> GetAll();
    }

    public class WeatherRepository : IWeatherRepository
    {
        private RepositoryHelper _repositoryHelper;
        private WeatherValidator _weatherValidator;

        public WeatherRepository(RepositoryHelper repositoryHelper, WeatherValidator weatherValidator) 
        {
            _repositoryHelper= repositoryHelper;
            _weatherValidator = weatherValidator;
        }

        #region IAlgorithmDispatchRepository Methods

        public Weather Get(string macrocellId, string typeId, DateTime localDatetime) 
        {
            return _repositoryHelper.GetWeather(macrocellId, typeId, localDatetime);
        }

        public IList<Weather> GetByMacrocellTypeAndDateTimeRange(String macrocellId, String measureTypeId, DateTime startLocalDateTime, DateTime endLocalDateTime)
        {
            return _repositoryHelper.GetBymacroCellTypeAndDateTimeRange(macrocellId, measureTypeId, startLocalDateTime, endLocalDateTime);
        }

        public IList<Weather> GetAll()
        {
            return _repositoryHelper.GetAllWeather();
        }

        public bool Exists(Weather weather) 
        {
            return Get(weather.MacrocellId,weather.MeasureTypeId,weather.LocalDateTime) != null;
        }
        public void Save(Weather weather)
        {
             _weatherValidator.Validate(weather);
             _repositoryHelper.SaveWeather(weather);
        }
        public void SaveList(IList<Weather> weatherList) 
        {
            _weatherValidator.ValidateList(weatherList);
            _repositoryHelper.SaveWeatherList(weatherList);
        }
        public void Update(Weather weather) 
        {
            _repositoryHelper.UpdateWeather(weather);
        }
       



        #endregion

    }
}
