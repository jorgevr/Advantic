using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface IManufacturerRepository
    {
        bool Exists(Manufacturer manufacturer);
        void Save(Manufacturer manufacturer);
        void SaveList(IList<Manufacturer> manufacturerList);
        void Update(Manufacturer manufacturer);
        Manufacturer Get(String manufacturerId);
        IList<Manufacturer> GetAll();
        
    }

    public class ManufacturerRepository : IManufacturerRepository
    {
        private RepositoryHelper _repositoryHelper;


        public ManufacturerRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region IManufacturerRepository Methods

        public Manufacturer Get(string manufacturerId) 
        {
            return _repositoryHelper.GetManufacturer(manufacturerId);
        }

        public IList<Manufacturer> GetAll()
        {
            return _repositoryHelper.GetAllManufacturer();
        }

        public bool Exists(Manufacturer manufacturer) 
        {
            return Get(manufacturer.Id) != null;
        }
        public void Save(Manufacturer manufacturer)
        {
            _repositoryHelper.SaveManufacturer(manufacturer);
        }
        public void SaveList(IList<Manufacturer> manufacturerList) 
        {
            _repositoryHelper.SaveManufacturerList(manufacturerList);
        }
        public void Update(Manufacturer manufacturer) 
        {
            _repositoryHelper.UpdateManufacturer(manufacturer);
        }
       
        #endregion

    }
}
