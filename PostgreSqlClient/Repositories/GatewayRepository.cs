using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface IGatewayRepository
    {
        bool Exists(Gateway gateway);
        void Save(Gateway gateway);
        void SaveList(IList<Gateway> gatewayList);
        void Update(Gateway gateway);
        Gateway Get(String gatewayId);
        IList<Gateway> GetAll();
        
    }

    public class GatewayRepository : IGatewayRepository
    {
        private RepositoryHelper _repositoryHelper;
        

        public GatewayRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region IGatewayRepository Methods

        public Gateway Get(string gatewayId) 
        {
            return _repositoryHelper.GetGateway(gatewayId);
        }

        public IList<Gateway> GetAll()
        {
            return _repositoryHelper.GetAllGateway();
        }

        public bool Exists(Gateway gateway) 
        {
            return Get(gateway.Id) != null;
        }
        public void Save(Gateway gateway)
        {
            _repositoryHelper.SaveGateway(gateway);
        }
        public void SaveList(IList<Gateway> gatewayList) 
        {
            _repositoryHelper.SaveGatewayList(gatewayList);
        }
        public void Update(Gateway gateway) 
        {
            _repositoryHelper.UpdateGateway(gateway);
        }
       
        #endregion

    }
}
