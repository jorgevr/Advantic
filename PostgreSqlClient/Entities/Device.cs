using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class Device
    {
        #region Public Virtual Properties

        public virtual String  Id { get; set; }
        public virtual String Description { get; set; }
        public virtual Appliance Appliance { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }
        public virtual Boolean OutPut { get; set; }
        public virtual Gateway Gateway { get; set; }
         

        #endregion

        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Device d = obj as Device;
            return d != null
                && d.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

    }
}
