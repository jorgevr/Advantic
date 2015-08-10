using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class SubCategory
    {
        #region Public Virtual Properties

        public virtual String Id { get; set; }
        public virtual Category Category{ get; set; }
        public virtual String Description { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }

        #endregion

        public SubCategory(string subCategoryId) 
        {
            Id = subCategoryId;
        }

        public SubCategory()
        {
        }

        #region Public Override Methods

        public override bool Equals(object obj)
        {
            SubCategory s = obj as SubCategory;
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
