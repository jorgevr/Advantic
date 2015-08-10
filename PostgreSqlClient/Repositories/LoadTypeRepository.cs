using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface ILoadTypeRepository
    {
        bool Exists(LoadType loadType);
        void Save(LoadType loadType);
        void SaveList(IList<LoadType> loadTypeList);
        void Update(LoadType loadType);
        LoadType Get(String loadTypeId);
        IList<LoadType> GetAll();
        
    }

    public class LoadTypeRepository : ILoadTypeRepository
    {
        private RepositoryHelper _repositoryHelper;


        public LoadTypeRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }


        #region ILoadTypeRepository Methods

        public LoadType Get(string loadTypeId) 
        {
            return _repositoryHelper.GetLoadType(loadTypeId);
        }

        public IList<LoadType> GetAll()
        {
            return _repositoryHelper.GetAllLoadType();
        }

        public bool Exists(LoadType loadType) 
        {
            return Get(loadType.Id) != null;
        }
        public void Save(LoadType loadType)
        {
            _repositoryHelper.SaveLoadType(loadType);
        }
        public void SaveList(IList<LoadType> loadTypeList) 
        {
            _repositoryHelper.SaveLoadTypeList(loadTypeList);
        }
        public void Update(LoadType loadType) 
        {
            _repositoryHelper.UpdateLoadType(loadType);
        }
       
        #endregion

    }
}
