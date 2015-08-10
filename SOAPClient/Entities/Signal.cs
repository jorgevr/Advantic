using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuresAdvanticMiddlewareDownloader.Entities
{
    public class SignalResponse
    {
        public virtual List<Signal> senales { get; set; }
    }

    public class Signal
    {
        #region Public Virtual Properties

        public virtual int id { get; set; }
        public virtual string senal { get; set; }
        public virtual int register { get; set; }
        


        #endregion

        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Signal p = obj as Signal;
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
