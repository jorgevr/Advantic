using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using log4net;
using Ninject;
using MeasuresAdvanticMiddlewareDownloader.Ninject;

namespace MeasuresAdvanticMiddlewareDownloader
{
    static class Program
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            initLogger();
            _logger.Debug("configured Logger");
            try
            {
                var kernel = new StandardKernel(new AdvanticMeasureNijectModule());
                var service = kernel.Get<MeasuresAdvanticMiddlewareDownloaderWindowsService>();
                ServiceBase.Run(service);
            }
            catch (Exception e)
            {
                _logger.Error("Exception while launching AdvanticMeasureService", e);
            }
        }

        private static void initLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

    }
}
