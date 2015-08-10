using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class LoadType
    {
        #region Public Virtual Properties

        public virtual String Id { get; set; }
        public virtual String Description { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }

        #endregion

        public LoadType(string loadTypeId) 
        {
            Id = loadTypeId;
        }

        public LoadType()
        {
        }


        #region Public Override Methods

        public override bool Equals(object obj)
        {
            LoadType l = obj as LoadType;
            return l != null
                && l.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }
}
