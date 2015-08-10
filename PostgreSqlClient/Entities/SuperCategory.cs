using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class SuperCategory
    {
        #region Public Virtual Properties

        public virtual String Id { get; set; }
        public virtual String Description { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }

        #endregion

        public SuperCategory(string superCategoryId) 
        {
            Id = superCategoryId;
        }

        public SuperCategory()
        {
        }


        #region Public Override Methods

        public override bool Equals(object obj)
        {
            SuperCategory s = obj as SuperCategory;
            return s != null
                && s.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }
}
