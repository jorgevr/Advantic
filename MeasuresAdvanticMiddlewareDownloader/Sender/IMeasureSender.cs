using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gnarum.Gestensis.Core.Entities;

namespace MeasuresAdvanticMiddlewareDownloader.Sender
{
    interface IMeasureSender
    {
        string Name { get; }
        void SendMeasureList(IList<Measure> measureList);
    }
}
