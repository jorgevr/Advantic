using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class Cell
    {
        #region Public Virtual Properties

        public virtual String Id { get; set; }
        public virtual MacroCell Macrocell { get; set; }
        public virtual Double Latitude { get; set; }
        public virtual Double Longitude { get; set; }
        public virtual Double Surface { get; set; }
        public virtual Double Volumen { get; set; }
        public virtual int BuildingShadeCoefficient { get; set; }
        public virtual int WallInsulationThickness { get; set; }
        public virtual String Description { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }

        #endregion
        public Cell(string macroCellId) 
        {
            Id = macroCellId;
        }

        public Cell()
        {
        }
        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Cell c = obj as Cell;
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
