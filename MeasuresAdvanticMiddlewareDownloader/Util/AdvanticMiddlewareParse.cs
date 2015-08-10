using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuresAdvanticMiddlewareDownloader.Util
{
    public class AdvanticMiddlewareParse
    {

            #region Public Virtual Properties

            public virtual String SignalId { get; set; }
            public virtual String Table { get; set; }
            public virtual String DeviceId { get; set; }
            public virtual String MeasureType { get; set; }
            public virtual String UnitType { get; set; }
 
            #endregion

            #region Public Override Methods

            public override bool Equals(object obj)
            {
                AdvanticMiddlewareParse p = obj as AdvanticMiddlewareParse;
                return p != null
                    && p.SignalId == SignalId
                    && p.Table == Table
                    && p.DeviceId == DeviceId
                    && p.MeasureType == MeasureType
                    && p.UnitType == UnitType;
            }

            public override int GetHashCode()
            {
                return SignalId.GetHashCode();
            }

            #endregion
    }
}
