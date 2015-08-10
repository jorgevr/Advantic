using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Quartz;
using Quartz.Impl;

namespace MeasuresAdvanticMiddlewareDownloader.Quartz
{
    public interface ICronDispatcher : IDisposable
    {
        void Init();
    }

    public class Dispatcher : ICronDispatcher
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Dispatcher));

        private static IScheduler _scheduler;
        private static IAdvanticMeasureJobFactory _jobFactory;

        public Dispatcher(IAdvanticMeasureJobFactory jobFactory)
        {
            _scheduler = new StdSchedulerFactory().GetScheduler();
            _jobFactory = jobFactory;
        }

        public void Init()
        {
            try
            {
                _logger.Info("Starting Dispatcher");
                init();
            }
            catch (Exception e)
            {
                _logger.ErrorFormat("Exception while preparing jobs: {0}\nStackTrace: \n{1}\n", e.Message, e.StackTrace);
            }
        }

        private void init()
        {
            foreach (var advanticMeasureJob in _jobFactory.GetAdvanticMeasureJobs())
            {
                scheduleAdvanticMeasureJob(advanticMeasureJob);
            }
            _scheduler.Start();
        }

        private void scheduleAdvanticMeasureJob(AdvanticMeasureJob advanticMeasureJob)
        {
            _logger.InfoFormat("Scheduling job {0}", advanticMeasureJob.Name);
            _scheduler.ScheduleJob(advanticMeasureJob.JobDetail, advanticMeasureJob.Trigger);
        }

        public void Dispose()
        {
            _logger.Info("Disposing Dispatcher");
            if (_scheduler != null)
            {
                _scheduler.Shutdown(false);
                _scheduler = null;
            }
        }
    }
}
