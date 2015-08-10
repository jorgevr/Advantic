using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface IMeasureTypeRepository
    {
        bool Exists(MeasureType measureType);
        void Save(MeasureType measureType);
        void SaveList(IList<MeasureType>  measureTypeList);
        void Update(MeasureType measureType);
        MeasureType Get(String measureTypeId);
        IList<MeasureType> GetAll();
        
    }

    public class MeasureTypeRepository : IMeasureTypeRepository
    {
        private RepositoryHelper _repositoryHelper;


        public MeasureTypeRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region IMeasureTypeRepository Methods

        public MeasureType Get(string measureTypeId) 
        {
            return _repositoryHelper.GetMeasureType(measureTypeId);
        }

        public IList<MeasureType> GetAll()
        {
            return _repositoryHelper.GetAllMeasureType();
        }

        public bool Exists(MeasureType measureType) 
        {
            return Get(measureType.Id) != null;
        }
        public void Save(MeasureType measureType)
        {
            _repositoryHelper.SaveMeasureType(measureType);
        }
        public void SaveList(IList<MeasureType> measureTypeList) 
        {
            _repositoryHelper.SaveMeasureTypeList(measureTypeList);
        }
        public void Update(MeasureType measureType) 
        {
            _repositoryHelper.UpdateMeasureType(measureType);
        }
       
        #endregion

    }
}
