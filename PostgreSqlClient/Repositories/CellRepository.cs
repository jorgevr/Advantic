using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface ICellRepository
    {
        bool Exists(Cell cell);
        void Save(Cell cell);
        void SaveList(IList<Cell> cellList);
        void Update(Cell cell);
        Cell Get(String cellId);
        IList<Cell> GetAll();
        
    }

    public class CellRepository : ICellRepository
    {
        private RepositoryHelper _repositoryHelper;


        public CellRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region ICellRepository Methods

        public Cell Get(string cellId) 
        {
            return _repositoryHelper.GetCell(cellId);
        }

        public IList<Cell> GetAll()
        {
            return _repositoryHelper.GetAllCell();
        }

        public bool Exists(Cell cell) 
        {
            return Get(cell.Id) != null;
        }
        public void Save(Cell cell)
        {
             _repositoryHelper.SaveCell(cell);
        }
        public void SaveList(IList<Cell> cellList) 
        {
            _repositoryHelper.SaveCellList(cellList);
        }
        public void Update(Cell cell) 
        {
            _repositoryHelper.UpdateCell(cell);
        }
       
        #endregion

    }
}
