using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Gnarum.Gestensis.Core.Entities;
using MeasuresAdvanticMiddlewareDownloader.Providers;

namespace MeasuresAdvanticMiddlewareDownloader.Sender
{
    class MeasureApiInsertion
    {
        public IList<Measure> MeasureList { get; set; }
    }

    public class WebApiSender : IMeasureSender
    {
        public const string TYPE_NAME = "WEB_API_MEASURE";
        public const string API_POST_METHOD = "api/measure";

        public string Name
        {
            get { return TYPE_NAME; }
        }

        public void SendMeasureList(IList<Measure> measureList)
        {
            IList<Measure> sendList = new List<Measure>();
            foreach (Measure measure in measureList)
            {
                sendList.Add(measure);

                if (sendList.Count >= 1000)
                {
                    sendMeasureList(sendList);
                    sendList.Clear();
                }
            }
            sendMeasureList(sendList);
            sendList.Clear();
        }

        private void sendMeasureList(IList<Measure> sendList)
        {
            MeasureApiInsertion insert = new MeasureApiInsertion()
            {
                MeasureList = sendList
            };

            String api_uri = ConfigurationManager.AppSettings["webApiLocation"];
            string jsonStringOfSMeasures = ConvertMeasuresToJSON(sendList);
            IWebAPIProvider webApiProvider = new WebApiProvider();
            try
            {
                bool jsonSentSuccesfully = webApiProvider.PutJSONMeasureList(api_uri, jsonStringOfSMeasures);
            }
            catch
            {

            }

        }

        private string ConvertMeasuresToJSON(IList<Measure> ListOfSMeasures)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            StringBuilder sb = new StringBuilder("{\"MeasureList\":");
            sb.Append(js.Serialize(ListOfSMeasures));
            sb.Append("}");
            return sb.ToString();
        }
    }
}
