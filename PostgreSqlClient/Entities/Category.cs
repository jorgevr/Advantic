using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class Category
    {
        #region Public Virtual Properties

        public virtual String Id { get; set; }
        public virtual SuperCategory SuperCategory { get; set; }
        public virtual String Description { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }

        #endregion

         public Category(string categoryId) 
        {
            Id = categoryId;
        }

         public Category()
        {
        }

        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Category c = obj as Category;
            return c != null
                && c.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }
}
