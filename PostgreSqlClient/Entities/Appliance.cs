using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class Appliance
    {
        #region Public Virtual Properties

        public virtual String  Id { get; set; }
        public virtual Double Power { get; set; }
        public virtual SubCategory Subcategory { get; set; }
        public virtual String Description { get; set; }
        public virtual Double Investment { get; set; }
        public virtual Room Room { get; set; }
        public virtual DateTime ActivacionDateTime { get; set; }
        public virtual DateTime LeavingDateTime { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }
        public virtual LoadType Loadtype { get; set; }

        #endregion

        public Appliance(string applianceId) 
        {
            Id = applianceId;
        }

        public Appliance()
        {
        }


        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Appliance a = obj as Appliance;
            return a != null
                && a.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

    }
}
