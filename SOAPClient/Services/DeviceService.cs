using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using MeasuresAdvanticMiddlewareDownloader.Entities;
using System.Web.Script.Serialization;

namespace SOAPClient.Services
{
    public interface IDeviceService
    {
        IList<Device> GetDevicesByUserAndLocation(User user, Location location);
    }

    public class DeviceService : IDeviceService
    {
        public IList<Device>  GetDevicesByUserAndLocation(User user, Location location)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            AdvanticWS.DataControllerPortTypeClient client = new AdvanticWS.DataControllerPortTypeClient();
            string response = client.getDevices(user.UserName,location.id.ToString());
            DeviceResponse deviceResponse = serializer.Deserialize<DeviceResponse>(response);
            return deviceResponse.dispositivos;
        }

    }
}
