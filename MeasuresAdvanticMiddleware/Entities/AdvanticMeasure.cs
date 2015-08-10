using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeasuresAdvanticMiddlewareDownloader.Entities;

namespace MeasuresAdvanticMiddleware.Entities
{
    public class AdvanticMeasure
    {
        #region Public Virtual Properties
        public virtual Data Data { get; set; }
        public virtual AdvanticSignal AdvanticSignal { get; set; }
        #endregion

            #region Public Override Methods

            public override bool Equals(object obj)
            {
                AdvanticMeasure p = obj as AdvanticMeasure;
                return p != null
                    && p.Data == Data
                    && p.AdvanticSignal == AdvanticSignal;


            }

            public override int GetHashCode()
            {
                return Data.GetHashCode();
            }

            #endregion
        
    }
}
