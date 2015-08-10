using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface IDeviceRepository
    {
        bool Exists(Device device);
        void Save(Device device);
        void SaveList(IList<Device> deviceList);
        void Update(Device device);
        Device Get(String deviceId);
        IList<Device> GetAll();
        
    }

    public class DeviceRepository : IDeviceRepository
    {
        private RepositoryHelper _repositoryHelper;
        

        public DeviceRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region IDeviceDispatchRepository Methods

        public Device Get(string deviceId) 
        {
            return _repositoryHelper.GetDevice(deviceId);
        }

        public IList<Device> GetAll()
        {
            return _repositoryHelper.GetAllDevice();
        }

        public bool Exists(Device device) 
        {
            return Get(device.Id) != null;
        }
        public void Save(Device device)
        {
             _repositoryHelper.SaveDevice(device);
        }
        public void SaveList(IList<Device> deviceList) 
        {
            _repositoryHelper.SaveDeviceList(deviceList);
        }
        public void Update(Device device) 
        {
            _repositoryHelper.UpdateDevice(device);
        }
       
        #endregion

    }
}
