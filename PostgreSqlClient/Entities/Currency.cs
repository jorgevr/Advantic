using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class Currency
    {
        #region Public Virtual Properties

        public virtual String  Id { get; set; }
        public virtual String Description{ get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }

        #endregion
        public Currency()
        {
           
        }

        public Currency(String id) 
        {
            Id = id;
        }

        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Currency c = obj as Currency;
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
