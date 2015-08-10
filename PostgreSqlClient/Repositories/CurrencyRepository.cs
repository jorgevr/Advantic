using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface ICurrencyRepository
    {
        bool Exists(Currency currency);
        void Save(Currency currency);
        void SaveList(IList<Currency> currencyList);
        void Update(Currency currency);
        Currency Get(String currencyId);
        IList<Currency> GetAll();
        
    }

    public class CurrencyRepository : ICurrencyRepository
    {
        private RepositoryHelper _repositoryHelper;


        public CurrencyRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region ICurrencyRepository Methods

        public Currency Get(string currencyId) 
        {
            return _repositoryHelper.GetCurrency(currencyId);
        }

        public IList<Currency> GetAll()
        {
            return _repositoryHelper.GetAllCurrency();
        }

        public bool Exists(Currency currency) 
        {
            return Get(currency.Id) != null;
        }
        public void Save(Currency currency)
        {
            _repositoryHelper.SaveCurrency(currency);
        }
        public void SaveList(IList<Currency> currencyList) 
        {
            _repositoryHelper.SaveCurrencyList(currencyList);
        }
        public void Update(Currency currency) 
        {
            _repositoryHelper.UpdateCurrency(currency);
        }
       
        #endregion

    }
}
