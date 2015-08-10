using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class Gateway
    {
        #region Public Virtual Properties

        public virtual String Id { get; set; }
        public virtual String Description { get; set; }
        public virtual int MacAddress { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }

        #endregion
        public Gateway(string gatewayId) 
        {
            Id = gatewayId;
        }

        public Gateway()
        {
        }
        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Gateway g = obj as Gateway;
            return g != null
                && g.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }
}
