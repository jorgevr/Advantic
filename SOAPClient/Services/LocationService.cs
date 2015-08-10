using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using MeasuresAdvanticMiddlewareDownloader.Entities;
using System.Web.Script.Serialization;

namespace SOAPClient.Services
{
    public interface ILocationService
    {
        List<Location> GetLocationsByUser(User user);
        List<Location> GetLocationsByUser(String userId);
    }

    public class LocationService : ILocationService
    {
        
        public List<Location> GetLocationsByUser(User user)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            AdvanticWS.DataControllerPortTypeClient client = new AdvanticWS.DataControllerPortTypeClient();
            string response = client.getLocations(user.UserName);
            LocationResponse locationResponse = serializer.Deserialize<LocationResponse>(response);
            return locationResponse.localizaciones;
        }
        public List<Location> GetLocationsByUser(String userId)
        {
            User user = new User(1, userId, "Gnarum");
            return GetLocationsByUser(user); 
        }

    }
}
