using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeasuresAdvanticMiddlewareDownloader.Entities;

namespace MeasuresAdvanticMiddleware.Entities
{
    public class AdvanticSignal
    {
        #region Public Virtual Properties
        public Device Device { get; set; }
        public Signal Signal { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
        #endregion

            #region Public Override Methods

            public override bool Equals(object obj)
            {
                AdvanticSignal s = obj as AdvanticSignal;
                return s != null
                    && s.Location == Location
                    && s.Device == Device
                    && s.Signal == Signal
                    && s.User == User;

            }

            public override int GetHashCode()
            {
                return Signal.GetHashCode();
            }

            #endregion
        
    }
}
