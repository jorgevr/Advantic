using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class MacroCell
    {
        #region Public Virtual Properties

        public virtual String  Id { get; set; }
        public virtual String Description { get; set; }
        public virtual String Region { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }

        #endregion

        public MacroCell(string macroCellId) 
        {
            Id = macroCellId;
        }

        public MacroCell()
        {
        }

        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Device p = obj as Device;
            return p != null
                && p.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

    }
}
