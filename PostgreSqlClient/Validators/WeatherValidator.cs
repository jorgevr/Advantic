using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
        public interface IWeatherValidator
    {
        void Validate(Weather weather);
        void ValidateList(IList<Weather> weatherList);
    }

        public class WeatherValidator : IWeatherValidator
    {
        #region Private Static Properties

        private static int UTCHOUR_MINVALUE = 0;
        private static int UTCHOUR_MAXVALUE = 23;
        private static int UTCMINUTE_MINVALUE = UTCHOUR_MINVALUE;
        private static int UTCMINUTE_MAXVALUE = 59;
        private static int UTCSECOND_MINVALUE = UTCMINUTE_MINVALUE;
        private static int UTCSECOND_MAXVALUE = UTCMINUTE_MAXVALUE;

        #endregion

        #region IForecastValidator Methods

        public void Validate(Weather weather)
        {
            validateUtcHour(weather.LocalDateTime.Hour);
            validateUtcMinute((weather.LocalDateTime.Minute));
            validateUtcSecond((weather.LocalDateTime.Second));
        }

        public void ValidateList(IList<Weather> weatherList)
        {
            foreach (Weather weather in weatherList)
            {
                Validate(weather);
            }
        }

        #endregion

        #region Private Methods

        private void validateUtcSecond(int utcSecond)
        {
            if (utcSecond < UTCMINUTE_MINVALUE || utcSecond > UTCMINUTE_MAXVALUE)
                throw new ArgumentException(String.Format("The Utc Second must be between {0} and {1}", UTCSECOND_MINVALUE, UTCSECOND_MAXVALUE), "UtcSecond");
        }

        private void validateUtcMinute(int utcMinute)
        {
            if (utcMinute < UTCMINUTE_MINVALUE || utcMinute > UTCMINUTE_MAXVALUE)
                throw new ArgumentException(String.Format("The Utc Minute must be between {0} and {1}", UTCMINUTE_MINVALUE, UTCMINUTE_MAXVALUE), "UtcMinute");

        }

        private void validateUtcHour(int utcHour)
        {
            if (utcHour < UTCHOUR_MINVALUE || utcHour > UTCHOUR_MAXVALUE)
                throw new ArgumentException(String.Format("The Utc Hour must be between {0} and {1}", UTCHOUR_MINVALUE, UTCHOUR_MAXVALUE), "UtcHour");
        }

        #endregion
    }
}
