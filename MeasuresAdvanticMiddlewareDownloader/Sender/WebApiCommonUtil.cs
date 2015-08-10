using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace MeasuresAdvanticMiddlewareDownloader.Sender
{
    public class WebApiCommonUtil
    {
        private static object _lock = new object();

        public static T Get<T>(string urlAddition, params string[] paramList)
        {
            lock (_lock)
            {
                string webApiUrl;
                if (paramList.Count() > 0 && paramList[0].StartsWith("http"))
                {
                    webApiUrl = paramList[0];
                }
                else
                {
                    webApiUrl = ConfigurationManager.AppSettings["webApiLocation"];
                }

                StringBuilder fullURL = new StringBuilder(webApiUrl);
                fullURL.Append(string.Format(urlAddition, paramList));
                byte[] data;

                using (WebClient webClient = new WebClient())
                {
                    data = webClient.DownloadData(fullURL.ToString());
                }

                string str = Encoding.GetEncoding("utf-8").GetString(data);

                var result = new JavaScriptSerializer().Deserialize<T>(str);

                return result;
            }
        }

        public static bool Put(string urlAddition, string jsonString)
        {
            lock (_lock)
            {
                string webApiUrl = ConfigurationManager.AppSettings["webApiLocation"];
                return Put(webApiUrl, urlAddition, jsonString);
            }

        }
        public static bool Put(string webApiUrl, string urlAddition, string jsonString)
        {
            lock (_lock)
            {
                StringBuilder fullURL = new StringBuilder(webApiUrl);
                fullURL.Append(string.Format(urlAddition));
                byte[] byteData = Encoding.GetEncoding("utf-8").GetBytes(jsonString);
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(fullURL.ToString());
                request.Method = "PUT";
                request.ContentType = "application/json";
                request.ContentLength = byteData.Length;
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteData, 0, byteData.Length);
                }

                string returnString;
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    response.GetResponseStream();
                    returnString = response.StatusCode.ToString();
                }
                return returnString.Equals("NoContent");
            }
        }

        public static bool Post(string webApiUrl, string urlAddition, string jsonString)
        {
            StringBuilder fullURL = new StringBuilder(webApiUrl);
            fullURL.Append(string.Format(urlAddition));
            byte[] byteData = Encoding.GetEncoding("utf-8").GetBytes(jsonString);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(fullURL.ToString());
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = byteData.Length;
            request.KeepAlive = true;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteData, 0, byteData.Length);
            dataStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string returnString = response.StatusCode.ToString();

            return returnString.Equals("Created");
        }
    }
}

