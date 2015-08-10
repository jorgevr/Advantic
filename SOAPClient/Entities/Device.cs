using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuresAdvanticMiddlewareDownloader.Entities
{
    public class DeviceResponse
    {
        public virtual List<Device> dispositivos { get; set; }
    }

    public class Device
    {
        #region Public Virtual Properties
        public int id { get; set; }
        public string Dispositivo { get; set; }
        public Double lat { get; set; }
        public Double longt { get; set; }
        public int estado { get; set; }
        #endregion

        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Device p = obj as Device;
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
