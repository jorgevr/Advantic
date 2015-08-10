using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using log4net;
using MeasuresAdvanticMiddlewareDownloader.Quartz;


namespace MeasuresAdvanticMiddlewareDownloader
{
    public partial class MeasuresAdvanticMiddlewareDownloaderWindowsService : ServiceBase
    {
        
        private static readonly ILog _logger = LogManager.GetLogger(typeof(MeasuresAdvanticMiddlewareDownloaderWindowsService));
        private static ICronDispatcher _dispatcher;

        public MeasuresAdvanticMiddlewareDownloaderWindowsService(ICronDispatcher dispatcher)
        {
            InitializeComponent();
            _dispatcher = dispatcher;
        }

       
        protected override void OnStart(string[] args)
        {
            try
            {
                _logger.Info("STARTED");
                initDispatcher();
            }
            catch (Exception e)
            {
                _logger.ErrorFormat("Exception while launching dispatcher: {0}\nStackTrace: \n{1}\n", e.Message, e.StackTrace);
            }
        }

        protected override void OnStop()
        {
            _logger.Info("STOPPED");
            if (_dispatcher != null)
                _dispatcher.Dispose();

            base.OnStop();
        }

        private void initDispatcher()
        {
            _dispatcher.Init();
        }

    }
}
