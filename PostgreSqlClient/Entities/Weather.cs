using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class Weather
    {
        #region Public Virtual Properties

        public virtual DateTime LocalDateTime { get; set; }
        public virtual String MacrocellId { get; set; }
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
            Weather m = obj as Weather;
            return m != null
                && m.MacrocellId == MacrocellId
                && m.LocalDateTime==LocalDateTime
                && m.UnitTypeId==UnitTypeId
                && m.MeasureTypeId==MeasureTypeId;
        }

        public override int GetHashCode()
        {
            return MacrocellId.GetHashCode()
                * LocalDateTime.GetHashCode()
                * UnitTypeId.GetHashCode()
                * MeasureTypeId.GetHashCode();
        }

        #endregion
    }
}
