using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MeasuresAdvanticMiddlewareDownloader.Entities;
using Quartz;
using log4net;
using MeasuresAdvanticMiddlewareDownloader.Configuration;
using MeasuresAdvanticMiddleware.Entities;
using MeasuresAdvanticMiddleware.Services;
using System.IO;
using MeasuresAdvanticMiddlewareDownloader.Util;
using Gnarum.Gestensis.Core.Entities;
using MeasuresAdvanticMiddlewareDownloader.Sender;
//using PostgreSqlClient.ConnectionProvider;
//using PostgreSqlClient.Repositories;
//using PostgreSqlClient.Entities;

namespace MeasuresAdvanticMiddlewareDownloader.Quartz
{
    [DisallowConcurrentExecution]
    public class AdvanticMeasureJob : IJob
    {
        const string DATETIMEFORMAT = "yyyy-MM-dd HH:mm:ss";
        private static IConfigurationProvider _configProvider;
        private static IAdvanticSignalService _advanticSignalService;
        private static IAdvanticMeasureService _advanticMeasureService;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AdvanticMeasureJob));

        public string Name { get; set; }
        public ITrigger Trigger { get; set; }
        public IJobDetail JobDetail { get; set; }

        

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.Info("FileProcesserJob started");
                execute(context);
                _logger.Info("FileProcesserJob ended");
            }
            catch (Exception e)
            {
                _logger.ErrorFormat("Exception while executing FileProcesserJob: {0}\nStackTrace: \n{1}\n", e.Message, e.StackTrace);
            }
        }

        private void execute(IJobExecutionContext context)
        {
            _logger.InfoFormat("Ejecutando Proceso");
            prepareContextData(context);
            refreshConfigSettings();
            List<AdvanticMiddlewareParse> dictionary = getDictionary();
            List<AdvanticMeasure> advanticMeasureList = getAdvanticMeasureList(dictionary);
            List<Measure> measureList = ParseAdvanticMeasureListToMeasure(advanticMeasureList, dictionary);
            //List<Weather> weatherList = ParseAdvanticMeasureListToWeather(advanticMeasureList, dictionary);
            //try
            //{
               // InsertMeasureInBD(measureList, weatherList);
            //}
            //catch
            //{
            //    _logger.Error("Error Insertando en BD");
            //}
            WriteMeasureList(measureList);
            //WebApiSender webApiSender = new WebApiSender();
            //webApiSender.SendMeasureList(measureList);
            
        }

       

        //private void InsertMeasureInBD(List<Measure> measureList, List<Weather>weatherList )
        //{
        //    PostgreSqlConnectionProvider connection = new PostgreSqlConnectionProvider();
        //    connection.InitializeConnection();
        //    PostgreSqlClient.Repositories.RepositoryHelper repository = new RepositoryHelper(connection);
        //    MeasureValidator measureValidator = new MeasureValidator();
        //    WeatherValidator weatherValidator= new WeatherValidator();
        //    MeasureRepository measureRepository = new MeasureRepository(repository, measureValidator);
        //    WeatherRepository weatherRepository= new WeatherRepository(repository, weatherValidator);
        //    measureValidator.ValidateList(measureList);
        //    weatherValidator.ValidateList(weatherList);
        //    measureRepository.SaveList(measureList);
        //    weatherRepository.SaveList(weatherList);
        //    connection.EndConnection();
        //    //WriteMeasureList(measureList);
        //}

        private List<Measure> ParseAdvanticMeasureListToMeasure(List<AdvanticMeasure> advanticMeasureList, List<AdvanticMiddlewareParse> dictionary)
        {
            List<Measure>measureList = new List<Measure>();
            foreach (AdvanticMeasure advanticMeasure in advanticMeasureList)
            {
                AdvanticMiddlewareParse word =
                    dictionary.First(x => x.SignalId == advanticMeasure.AdvanticSignal.Signal.id.ToString());
                if (word.Table == "MEASURE")
                {
                    Gnarum.Gestensis.Core.Entities.Signal signal= getSignal(word,advanticMeasure);
                    DateTime utcDateTime=getUtcDateTime(advanticMeasure.Data.fecha);
                    ReliabilityType reliabilityType = getReliabilityType(); 
                    measureList.Add(Measure.Create(signal,utcDateTime,advanticMeasure.Data.valor,reliabilityType,1,DateTime.UtcNow,"ADVANTICSERVICE"));
                    
                    //measureList.Add(new Measure()
                    //{
                    //    LocalDateTime = advanticMeasure.Data.fecha,
                    //    Value = advanticMeasure.Data.valor,
                    //    DeviceId = word.DeviceId,
                    //    UnitTypeId = word.UnitType,
                    //    MeasureTypeId = word.MeasureType,
                    //    LocalInsertTime = DateTime.Now,
                    //    InsertUser = "ADVANTICMIDDLEWARESERVICE",
                    //    UpdateUser = "ADVANTICMIDDLEWARESERVICE",
                    //    UpdateLocalDateTime = DateTime.Now
                    //});
                }
            }
            return measureList;
        }

        private ReliabilityType getReliabilityType()
        {
            return ReliabilityType.Create("0", "VALID", "VALID", true, DateTime.UtcNow, "DVELASCO");
        }

        private DateTime getUtcDateTime(DateTime localDateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(localDateTime,TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time"));
        }

        private Gnarum.Gestensis.Core.Entities.Signal getSignal(AdvanticMiddlewareParse word, AdvanticMeasure advanticMeasure)
        {
            Gnarum.Gestensis.Core.Entities.Device device = getDevice(advanticMeasure);
            Gnarum.Gestensis.Core.Entities.Source source = getSource();
            Gnarum.Gestensis.Core.Entities.DataVariable datavariable = getDatavariable(advanticMeasure);
            Gnarum.Gestensis.Core.Entities.Resolution resolution = getResolution();

            return Gnarum.Gestensis.Core.Entities.Signal.Create
                (
                advanticMeasure.AdvanticSignal.Signal.id.ToString(),
                device,
                source,
                datavariable,
                resolution,
                advanticMeasure.AdvanticSignal.Signal.senal,
                advanticMeasure.AdvanticSignal.Signal.senal,
                true,
                0,
                DateTime.UtcNow,
                "DVELASCO"
                ); 
        }

        private Resolution getResolution()
        {
            return Resolution.Create("ResolutionId","ResolutionDescription",0,true);
        }

        private DataVariable getDatavariable(AdvanticMeasure advanticMeasure)
        {
            return DataVariable.Create("DatavariableId", "DatavariableName", "DatavariableDescription", advanticMeasure.Data.unit, true);
        }

        private Source getSource()
        {
            return Source.Create("ADV", "ADVANTIC", "ADVANTIC", true, DateTime.UtcNow, "DVELASCO");
        }

        private Gnarum.Gestensis.Core.Entities.Device getDevice(AdvanticMeasure advanticMeasure)
        {
            Gnarum.Gestensis.Core.Entities.TypeDevice typeDevice = getTypeDevice();
            Gnarum.Gestensis.Core.Entities.Application application = getApplication();
            Gnarum.Gestensis.Core.Entities.Manufacture manufacture = getManufacture();
            return Gnarum.Gestensis.Core.Entities.Device.Create
                (
                advanticMeasure.AdvanticSignal.Device.id.ToString(),
                typeDevice,
                advanticMeasure.AdvanticSignal.Device.Dispositivo,
                advanticMeasure.AdvanticSignal.Device.Dispositivo,
                true,
                application,
                manufacture,
                DateTime.UtcNow,
                "DVELASCO");
        }

        private TypeDevice getTypeDevice()
        {
            return TypeDevice.Create("TypeDeviceID", "TypeDeviceName", "TypeDeviceDescription", true, DateTime.UtcNow, "DVELASCO");
        }

        private Manufacture getManufacture()
        {
            return Manufacture.Create("ManufactureID", "ManufactureName", "ManufactureDescription", true, DateTime.UtcNow, "DVELASCO");
        }

        private Application getApplication()
        {
            return Application.Create("ApplicationID", "ApplicationName", "ApplicationDescription", true, DateTime.UtcNow, "DVELASCO");
        }

        //private List<Weather> ParseAdvanticMeasureListToWeather(List<AdvanticMeasure> advanticMeasureList, List<AdvanticMiddlewareParse> dictionary)
        //{
        //    List<Weather> weatherList = new List<Weather>();
        //    foreach (AdvanticMeasure advanticMeasure in advanticMeasureList)
        //    {
        //        AdvanticMiddlewareParse word =
        //            dictionary.First(x => x.SignalId == advanticMeasure.AdvanticSignal.Signal.id.ToString());
        //        if (word.Table == "WEATHER")
        //        {
        //            weatherList.Add(new Weather()
        //            {
        //                LocalDateTime = advanticMeasure.Data.fecha,
        //                Value = advanticMeasure.Data.valor,
        //                MacrocellId = word.DeviceId,
        //                UnitTypeId = word.UnitType,
        //                MeasureTypeId = word.MeasureType,
        //                LocalInsertTime = DateTime.Now,
        //                InsertUser = "ADVANTICMIDDLEWARESERVICE",
        //                UpdateUser = "ADVANTICMIDDLEWARESERVICE",
        //                UpdateLocalDateTime = DateTime.Now
        //            });
        //        }
        //    }
        //    return weatherList;
        //}

        private void WriteMeasureList(IList<Measure> measureList)
        {
            var delimiter = ";";
            var fullPath = "D://Test/Measure.csv";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(delimiter, "ZISIGNALID", "ZIUTCDATETIME", "ZIVALUE", "ZIRELIABTYPE", "ZIPERCENTAGE", "ZIUTCDTINSERT", "ZIUSER"));
            foreach (Measure measure in measureList)
            {
                sb.AppendLine(string.Join(delimiter,
                    measure.Signal.Id, 
                    measure.UtcDateTime.ToString("YYYYMMddHHmmss"),
                    measure.Value, measure.ReliabilityType.Id,
                    measure.Percentage,
                    measure.UtcDateTime.ToString("YYYYMMddHHmmss"),
                    measure.User));
            }
            File.WriteAllText(fullPath, sb.ToString());
            
        }

        

        private List<AdvanticMiddlewareParse> getDictionary()
        {
            var reader = new StreamReader(File.OpenRead(@"D://Test/Dictionary//dictionary.csv"));
            List<AdvanticMiddlewareParse> dictionary = new List<AdvanticMiddlewareParse>();
            int lineCont = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                //Cabecera
                if (lineCont != 0)
                    dictionary.Add(getAdvanticMiddlewareParse(line.Split(';')));  
                lineCont++;
            }
            return dictionary;
        }

        private AdvanticMiddlewareParse getAdvanticMiddlewareParse(string[] values)
        {
            return new AdvanticMiddlewareParse()
            {
                SignalId = values[0],
                Table = values[1],
                DeviceId = values[2],
                MeasureType = values[3],
                UnitType = values[4]
            };
        }

        private List<AdvanticMeasure> getAdvanticMeasureList(List<AdvanticMiddlewareParse>dictionary )
        {
            _logger.InfoFormat("Getting Advantic Measure");
            string userId = _configProvider.GetAdvanticUser();
            DateTime startLocalDateTime = getStartLocaldateTime();
            DateTime endLocalDateTime = getEndLocaldateTime();
            _logger.InfoFormat(String.Format("UserId: {0},startLocalDateTime:{1}, endLocalDateTime:{2}", userId, startLocalDateTime, endLocalDateTime));
            List<AdvanticSignal> advanticSignalList = _advanticSignalService.GetAdvanticSignal(userId).ToList();
            _logger.InfoFormat(String.Format("advanticSignalList: {0}", advanticSignalList.Count));
            //foreach (AdvanticSignal signal in advanticSignalList.OrderBy(x=>x.Signal.id))
            //{
            //    _logger.InfoFormat(String.Format("AdvanticSignal:{0}", signal.Signal.id));
            //    _logger.InfoFormat(String.Format("Existe en dictionary:{0}",
            //        dictionary.Exists(z => z.SignalId == signal.Signal.id.ToString())));
            //}
          
            
            List<AdvanticSignal> advanticSignalListInDictionary = advanticSignalList.Where(x=>dictionary.Exists(z=>z.SignalId==x.Signal.id.ToString())).ToList();
            _logger.InfoFormat(String.Format("AdvanticSignalListInDictionaryCount: {0}", advanticSignalListInDictionary.Count));
            List<AdvanticMeasure> advanticMeasureList = _advanticMeasureService.GetMeasureByAdvanticSignalListBetweenLocalDates(advanticSignalListInDictionary, startLocalDateTime, endLocalDateTime).ToList();
            _logger.InfoFormat(String.Format("Get {0} Advantic Measure", advanticMeasureList.Count));
            return advanticMeasureList;
        }

        

        private void WriteAdvanticMeasureList(List<AdvanticMeasure> advanticMeasureList)
        {
            var delimiter = ";";
            var fullPath="D://Test/test.csv";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Join(delimiter, "DeviceId", "SignalId","Señal", "Fecha", "Valor", "Unidad"));
            foreach (AdvanticMeasure advanticMeasure in advanticMeasureList)
            {
                sb.AppendLine(string.Join(delimiter, advanticMeasure.AdvanticSignal.Device.id,advanticMeasure.AdvanticSignal.Signal.id, advanticMeasure.AdvanticSignal.Signal.senal, advanticMeasure.Data.fecha.ToString(DATETIMEFORMAT), advanticMeasure.Data.valor, advanticMeasure.Data.unit));
            }
            File.WriteAllText(fullPath, sb.ToString());
        }

        private DateTime getStartLocaldateTime()
        {
            DateTime referenceDate = _configProvider.GetReferenceDate();
            int daysAfterReferenceDate = _configProvider.GetDaysAfterReferenceDate();
            return referenceDate.AddHours(daysAfterReferenceDate);
        }

        private DateTime getEndLocaldateTime()
        {
            DateTime referenceDate = _configProvider.GetReferenceDate();
            int daysBeforeReferenceDate = _configProvider.GetDaysBeforeReferenceDate();
            return referenceDate.AddHours(daysBeforeReferenceDate);
        }

        private void refreshConfigSettings()
        {
            _logger.Debug("Refreshing config file cache");
            _configProvider.Refresh();
            _logger.Debug("Done refreshing config file cache");
        }

        private void prepareContextData(IJobExecutionContext context)
        {
            _logger.Info("Getting Job Data from context");
            var confProvider = context.Trigger.JobDataMap.Get("ConfigurationProvider") as IConfigurationProvider;
            if (confProvider == null)
                throw new ArgumentException("Cant get IConfigurationProvider from context");
            _configProvider = confProvider;
            var advanticMeasureService = context.Trigger.JobDataMap.Get("AdvanticMeasureService") as IAdvanticMeasureService;
            if (advanticMeasureService == null)
                throw new ArgumentException("Cant get IAdvanticMeasureService from context");
            _advanticMeasureService = advanticMeasureService;
            var advanticSignalService = context.Trigger.JobDataMap.Get("AdvanticSignalService") as IAdvanticSignalService;
            if (advanticSignalService == null)
                throw new ArgumentException("Cant get IAdvanticSignalService from context");
            _advanticSignalService = advanticSignalService;
            
        }
    }
}
