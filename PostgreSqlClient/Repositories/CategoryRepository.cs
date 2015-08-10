using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface ICategoryRepository
    {
        bool Exists(Category category);
        void Save(Category category);
        void SaveList(IList<Category> categoryList);
        void Update(Category category);
        Category Get(String categoryId);
        IList<Category> GetAll();
        
    }

    public class CategoryRepository : ICategoryRepository
    {
        private RepositoryHelper _repositoryHelper;


        public CategoryRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region ICategoryRepository Methods

        public Category Get(string categoryId) 
        {
            return _repositoryHelper.GetCategory(categoryId);
        }

        public IList<Category> GetAll()
        {
            return _repositoryHelper.GetAllCategory();
        }

        public bool Exists(Category category) 
        {
            return Get(category.Id) != null;
        }
        public void Save(Category category)
        {
            _repositoryHelper.SaveCategory(category);
        }
        public void SaveList(IList<Category> categoryList) 
        {
            _repositoryHelper.SaveCategoryList(categoryList);
        }
        public void Update(Category category) 
        {
            _repositoryHelper.UpdateCategory(category);
        }
       
        #endregion

    }
}
