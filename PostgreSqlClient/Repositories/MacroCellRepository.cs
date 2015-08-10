using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface IMacroCellRepository
    {
        bool Exists(MacroCell macrocell);
        void Save(MacroCell macrocell);
        void SaveList(IList<MacroCell> macrocellList);
        void Update(MacroCell macrocell);
        MacroCell Get(String macrocellId);
        IList<MacroCell> GetAll();
        
    }

    public class MacroCellRepository : IMacroCellRepository
    {
        private RepositoryHelper _repositoryHelper;
        

        public MacroCellRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region IMacrocellRepository Methods

        public MacroCell Get(string macrocellId) 
        {
            return _repositoryHelper.GetMacroCell(macrocellId);
        }

        public IList<MacroCell> GetAll()
        {
            return _repositoryHelper.GetAllMacroCell();
        }

        public bool Exists(MacroCell macrocell) 
        {
            return Get(macrocell.Id) != null;
        }
        public void Save(MacroCell macrocell)
        {
             _repositoryHelper.SaveMacroCell(macrocell);
        }
        public void SaveList(IList<MacroCell> macrocellList) 
        {
            _repositoryHelper.SaveMacroCellList(macrocellList);
        }
        public void Update(MacroCell macrocell) 
        {
            _repositoryHelper.UpdateMacroCell(macrocell);
        }
       
        #endregion

    }
}
