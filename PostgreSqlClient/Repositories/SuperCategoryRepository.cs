using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface ISuperCategoryRepository
    {
        bool Exists(SuperCategory supercategory);
        void Save(SuperCategory supercategory);
        void SaveList(IList<SuperCategory> supercategoryList);
        void Update(SuperCategory supercategory);
        SuperCategory Get(String supercategoryId);
        IList<SuperCategory> GetAll();
        
    }

    public class SuperCategoryRepository : ISuperCategoryRepository
    {
        private RepositoryHelper _repositoryHelper;


        public SuperCategoryRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region ISuperCategoryRepository Methods

        public SuperCategory Get(string supercategoryId) 
        {
            return _repositoryHelper.GetSuperCategory(supercategoryId);
        }

        public IList<SuperCategory> GetAll()
        {
            return _repositoryHelper.GetAllSuperCategory();
        }

        public bool Exists(SuperCategory supercategory) 
        {
            return Get(supercategory.Id) != null;
        }
        public void Save(SuperCategory supercategory)
        {
            _repositoryHelper.SaveSuperCategory(supercategory);
        }
        public void SaveList(IList<SuperCategory> supercategoryList) 
        {
            _repositoryHelper.SaveSuperCategoryList(supercategoryList);
        }
        public void Update(SuperCategory supercategory) 
        {
            _repositoryHelper.UpdateSuperCategory(supercategory);
        }
       
        #endregion

    }
}
