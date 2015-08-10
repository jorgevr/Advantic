using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface ISubCategoryRepository
    {
        bool Exists(SubCategory subcategory);
        void Save(SubCategory subcategory);
        void SaveList(IList<SubCategory> subcategoryList);
        void Update(SubCategory subcategory);
        SubCategory Get(String subcategoryId);
        IList<SubCategory> GetAll();
        
    }

    public class SubCategoryRepository : ISubCategoryRepository
    {
        private RepositoryHelper _repositoryHelper;


        public SubCategoryRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region ISubCategoryRepository Methods

        public SubCategory Get(string subcategoryId) 
        {
            return _repositoryHelper.GetSubCategory(subcategoryId);
        }

        public IList<SubCategory> GetAll()
        {
            return _repositoryHelper.GetAllSubCategory();
        }

        public bool Exists(SubCategory subcategory) 
        {
            return Get(subcategory.Id) != null;
        }
        public void Save(SubCategory subcategory)
        {
            _repositoryHelper.SaveSubCategory(subcategory);
        }
        public void SaveList(IList<SubCategory> subcategoryList) 
        {
            _repositoryHelper.SaveSubCategoryList(subcategoryList);
        }
        public void Update(SubCategory subcategory) 
        {
            _repositoryHelper.UpdateSubCategory(subcategory);
        }
       
        #endregion

    }
}
