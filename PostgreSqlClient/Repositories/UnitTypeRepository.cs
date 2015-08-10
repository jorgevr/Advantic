using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface IUnitTypeRepository
    {
        bool Exists(UnitType unitType);
        void Save(UnitType unitType);
        void SaveList(IList<UnitType> unitTypeList);
        void Update(UnitType unitType);
        UnitType Get(String unitTypeId);
        IList<UnitType> GetAll();
        
    }

    public class UnitTypeRepository : IUnitTypeRepository
    {
        private RepositoryHelper _repositoryHelper;
        

        public UnitTypeRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region IIUnitTypeRepository Methods

        public UnitType Get(string unitTypeId) 
        {
            return _repositoryHelper.GetUnitType(unitTypeId);
        }

        public IList<UnitType> GetAll()
        {
            return _repositoryHelper.GetAllUnitType();
        }

        public bool Exists(UnitType unitType) 
        {
            return Get(unitType.Id) != null;
        }
        public void Save(UnitType unitType)
        {
            _repositoryHelper.SaveUnitType(unitType);
        }
        public void SaveList(IList<UnitType> unitTypeList) 
        {
            _repositoryHelper.SaveUnitTypeList(unitTypeList);
        }
        public void Update(UnitType unitType) 
        {
            _repositoryHelper.UpdateUnitType(unitType);
        }
       
        #endregion

    }
}
