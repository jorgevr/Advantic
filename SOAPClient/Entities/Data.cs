using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuresAdvanticMiddleware.Entities
{
    public class DataResponse
    {
        public virtual List<Data> lecturas { get; set; }
    }

    public class Data
    {
        #region Public Virtual Properties

        public virtual DateTime fecha { get; set; }
        public virtual Double valor { get; set; }
        public virtual string desc { get; set; }
        public virtual string name { get; set; }
        public virtual string unit { get; set; }
        public virtual string descunit { get; set; }
        #endregion

        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Data p = obj as Data;
            return p != null
                && p.fecha == fecha
                && p.desc==desc
                && p.valor==valor
                && p.name==name
                && p.unit==unit
                && p.descunit==descunit;
        }

        public override int GetHashCode()
        {
            return desc.GetHashCode();
        }

        #endregion
    }
}
