using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface IApplianceRepository
    {
        bool Exists(Appliance appliance);
        void Save(Appliance appliance);
        void SaveList(IList<Appliance> appliance);
        void Update(Appliance appliance);
        Appliance Get(String applianceId);
        IList<Appliance> GetAll();
        
    }

    public class ApplianceRepository : IApplianceRepository
    {
        private RepositoryHelper _repositoryHelper;


        public ApplianceRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region IApplianceRepository Methods

        public Appliance Get(string applianceId) 
        {
            return _repositoryHelper.GetAppliance(applianceId);
        }

        public IList<Appliance> GetAll()
        {
            return _repositoryHelper.GetAllAppliance();
        }

        public bool Exists(Appliance appliance) 
        {
            return Get(appliance.Id) != null;
        }
        public void Save(Appliance appliance)
        {
            _repositoryHelper.SaveAppliance(appliance);
        }
        public void SaveList(IList<Appliance> applianceList) 
        {
            _repositoryHelper.SaveApplianceList(applianceList);
        }
        public void Update(Appliance appliance) 
        {
            _repositoryHelper.UpdateAppliance(appliance);
        }
       
        #endregion

    }
}
