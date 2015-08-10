using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class Measure
    {
        #region Public Virtual Properties

        public virtual DateTime LocalDateTime { get; set; }
        public virtual String DeviceId { get; set; }
        public virtual String UnitTypeId{ get; set; }
        public virtual String MeasureTypeId { get; set; }
        public virtual Double Value { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }


        #endregion

        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Measure m = obj as Measure;
            return m != null
                && m.DeviceId==DeviceId
                && m.LocalDateTime==LocalDateTime
                && m.UnitTypeId == UnitTypeId
                && m.UnitTypeId == UnitTypeId
                && m.MeasureTypeId==MeasureTypeId;
        }

        public override int GetHashCode()
        {
            return  DeviceId.GetHashCode()
                * LocalDateTime.GetHashCode()
                * UnitTypeId.GetHashCode()
                * MeasureTypeId.GetHashCode();
        }

        #endregion
    }
}
