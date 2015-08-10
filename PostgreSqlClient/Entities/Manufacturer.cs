using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class Manufacturer
    {
        #region Public Virtual Properties

        public virtual String Id { get; set; }
        public virtual String Make { get; set; }
        public virtual String Model { get; set; }
        public virtual String Firmware { get; set; }
        public virtual String Description { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }

        #endregion
        public Manufacturer(string manufacturerId) 
        {
            Id = manufacturerId;
        }

         public Manufacturer()
        {
        }
        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Manufacturer m = obj as Manufacturer;
            return m != null
                && m.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }
}
