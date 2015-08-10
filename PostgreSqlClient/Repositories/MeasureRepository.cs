using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface IMeasureRepository
    {
        bool Exists(Measure measure);
        void Save(Measure measure);
        void SaveList(IList<Measure> measureList);
        void Update(Measure measure);
        Measure Get(String deviceId, String typeId, DateTime localDatetime);
        IList<Measure> GetByDeviceTypeAndDateTimeRange(String device, String measureType, DateTime startLocalDateTime, DateTime endLocalDateTime);
        IList<Measure> GetAll();
    }

    public class MeasureRepository : IMeasureRepository
    {
        private RepositoryHelper _repositoryHelper;
        private MeasureValidator _measureValidator;

        public MeasureRepository(RepositoryHelper repositoryHelper, MeasureValidator measureValidator) 
        {
            _repositoryHelper= repositoryHelper;
            _measureValidator = measureValidator;
        }

        #region IAlgorithmDispatchRepository Methods

        public Measure Get(string deviceId, string typeId, DateTime localDatetime) 
        {
            return _repositoryHelper.GetMeasure(deviceId, typeId, localDatetime);
        }

        public IList<Measure> GetByDeviceTypeAndDateTimeRange(String deviceId, String measureTypeId, DateTime startLocalDateTime, DateTime endLocalDateTime)
        {
            return _repositoryHelper.GetByDeviceTypeAndDateTimeRange(deviceId, measureTypeId, startLocalDateTime, endLocalDateTime);
        }

        public IList<Measure> GetAll()
        {
            return _repositoryHelper.GetAllMeasure();
        }

        public bool Exists(Measure measure) 
        {
            return Get(measure.DeviceId,measure.MeasureTypeId,measure.LocalDateTime) != null;
        }
        public void Save(Measure measure)
        {
             _measureValidator.Validate(measure);
             _repositoryHelper.SaveMeasure(measure);
        }
        public void SaveList(IList<Measure> measureList) 
        {
            _measureValidator.ValidateList(measureList);
            _repositoryHelper.SaveMeasureList(measureList);
        }
        public void Update(Measure measure) 
        {
            _repositoryHelper.UpdateMeasure(measure);
        }
       



        #endregion

    }
}
