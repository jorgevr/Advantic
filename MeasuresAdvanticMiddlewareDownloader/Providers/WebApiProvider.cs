using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeasuresAdvanticMiddlewareDownloader.Sender;

namespace MeasuresAdvanticMiddlewareDownloader.Providers
{
    public interface IWebAPIProvider
    {
        bool PutJSONMeasureList(string URL, string json);
    }
    class WebApiProvider : IWebAPIProvider
    {
        public bool PutJSONMeasureList(string URL, string json)
        {
            return WebApiCommonUtil.Put(URL, "/api/measure", json);
        }
    }

}
