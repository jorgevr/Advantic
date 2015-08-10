using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using MeasuresAdvanticMiddlewareDownloader.Entities;
using MeasuresAdvanticMiddleware.Entities;
using System.Web.Script.Serialization;
using SOAPClient.Services;


namespace MeasuresAdvanticMiddleware.Services
{
    public interface IAdvanticMeasureService
    {
        IList<AdvanticMeasure> GetMeasureByAdvanticSignalListBetweenLocalDates(List<AdvanticSignal> signalList, DateTime startLocalDateTime, DateTime endLocalDateTime);
    }

    public class AdvanticMeasureService : IAdvanticMeasureService
    {
        const string DATETIMEFORMAT = "yyyy-MM-dd HH:mm:ss";

        public IList<AdvanticMeasure> GetMeasureByAdvanticSignalListBetweenLocalDates(List<AdvanticSignal> signalList, DateTime startLocalDateTime, DateTime endLocalDateTime)
        {
            List<AdvanticMeasure> advanticMeasureList = new List<AdvanticMeasure>();
            DataService dataService = new DataService();
            foreach(AdvanticSignal signal in signalList.OrderBy(x=>x.Signal.id))
            {
                IList<Data> dataList = dataService.GetDataByUserAndSignalBetweenLocalDates(signal.User, signal.Signal, startLocalDateTime, endLocalDateTime);
                advanticMeasureList.AddRange(createAdvanticMeasureByAdvanticSignalAndData(signal, dataList));
            }

            return advanticMeasureList;


        }

        private IList<AdvanticMeasure> createAdvanticMeasureByAdvanticSignalAndData(AdvanticSignal signal, IList<Data> dataList)
        {
            List<AdvanticMeasure> advanticMeasureList = new List<AdvanticMeasure>();
            foreach (Data data in dataList)
            {
                advanticMeasureList.Add(new AdvanticMeasure()
                {
                    AdvanticSignal = signal,
                    Data = data
                });
            }
            return advanticMeasureList;
        }

    }
}
