using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;
using MeasuresAdvanticMiddlewareDownloader.Entities;
using System.Web.Script.Serialization;
using MeasuresAdvanticMiddleware.Entities;
using log4net;

namespace SOAPClient.Services
{
    public interface IDataService
    {
        IList<Data> GetDataByUserAndSignalBetweenLocalDates(User user,Signal signal, DateTime startLocalDateTime, DateTime endLocalDateTime);
    }

    public class DataService : IDataService
    {
        const string DATETIMEFORMAT = "yyyy-MM-dd HH:mm:ss";

        public IList<Data> GetDataByUserAndSignalBetweenLocalDates(User user, Signal signal, DateTime startLocalDateTime, DateTime endLocalDateTime)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            AdvanticWS.DataControllerPortTypeClient client = new AdvanticWS.DataControllerPortTypeClient();
            string response = client.getData(user.UserName, signal.id.ToString(), startLocalDateTime.ToString(DATETIMEFORMAT),endLocalDateTime.ToString(DATETIMEFORMAT), "M");
            DataResponse dataResponse = serializer.Deserialize<DataResponse>(response);
            return dataResponse.lecturas;
        }

    }
}
