using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeasuresAdvanticMiddlewareDownloader.Entities;
using System.Web.Script.Serialization;

namespace SOAPClient.Services
{
    public interface ISignalService
    {
        IList<Signal> GetSignalsByUserAndDevice(User user, Device device);
    }

    public class SignalService : ISignalService
    {
        public IList<Signal> GetSignalsByUserAndDevice(User user, Device device)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            AdvanticWS.DataControllerPortTypeClient client = new AdvanticWS.DataControllerPortTypeClient();
            string response = client.getSignals(user.UserName,device.id.ToString());
            SignalResponse signalResponse = serializer.Deserialize<SignalResponse>(response);
            return signalResponse.senales;
        }

    }
}
