using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuresAdvanticMiddlewareDownloader.Entities
{
    public class LocationResponse
    {
        public virtual List<Location> localizaciones { get; set; }
    }


    public class Location
    {
        #region Public Virtual Properties

        public virtual int id { get; set; }
        public virtual string localizacion { get; set; }
        public virtual string ciudad { get; set; }
        public virtual string pais { get; set; }
        

        #endregion

        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Location p = obj as Location;
            return p != null
                && p.id == id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        #endregion
    }
}
