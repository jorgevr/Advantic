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
    public interface IAdvanticSignalService
    {
        IList<AdvanticSignal> GetAdvanticSignal(String userId);
        IList<AdvanticSignal> GetAdvanticSignal(User user, Location location);
        IList<AdvanticSignal> GetAdvanticSignal(User user, Location location, Device device);
    }

    public class AdvanticsSignalService : IAdvanticSignalService
    {
        const string DATETIMEFORMAT = "yyyy-MM-dd HH:mm:ss";

        public IList<AdvanticSignal> GetAdvanticSignal(String userId)
        {
            List<AdvanticSignal> advanticSignalList = new List<AdvanticSignal>();
            LocationService locationService = new LocationService();
            User user= new User(userId);
            List<Location> locationList = locationService.GetLocationsByUser(user);
            foreach (Location location in locationList)
                advanticSignalList.AddRange(GetAdvanticSignal(user, location));
            return advanticSignalList;
        }

        public IList<AdvanticSignal> GetAdvanticSignal(User user, Location location)
        {
            List<AdvanticSignal> advanticSignalList = new List<AdvanticSignal>();
            DeviceService deviceService = new DeviceService();
            IList<Device> deviceList = deviceService.GetDevicesByUserAndLocation(user, location);
            foreach (Device device in deviceList)
                advanticSignalList.AddRange(GetAdvanticSignal(user,location,device));
            return advanticSignalList;
        }

        public IList<AdvanticSignal> GetAdvanticSignal(User user, Location location, Device device)
        {
            List<AdvanticSignal> advanticSignalList = new List<AdvanticSignal>();
            SignalService signalService = new SignalService();
            IList<Signal> signalList = signalService.GetSignalsByUserAndDevice(user, device);
            advanticSignalList.AddRange(createAdvanticSignalByUserAndLocationAndDeviceAndSignal(user, location, device,signalList));
            return advanticSignalList;
        }



        private IList<AdvanticSignal> createAdvanticSignalByUserAndLocationAndDeviceAndSignal(User user, Location location, Device device,IList<Signal> signalList)
        {
            List<AdvanticSignal> advanticSignalList = new List<AdvanticSignal>();
            foreach (Signal signal in signalList)
            {
                advanticSignalList.Add(new AdvanticSignal()
                {
                    User=user,
                    Location=location,
                    Device=device,
                    Signal=signal
                });
            }
            return advanticSignalList;
        }

    }
}
