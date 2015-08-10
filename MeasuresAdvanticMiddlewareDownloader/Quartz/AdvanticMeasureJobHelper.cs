using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Quartz;
using MeasuresAdvanticMiddlewareDownloader.Configuration;
using MeasuresAdvanticMiddleware.Services;

namespace MeasuresAdvanticMiddlewareDownloader.Quartz
{
    class AdvanticMeasureJobHelper : IAdvanticMeasureJobHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AdvanticMeasureJobHelper));
        private static IConfigurationProvider _configProvider;
        private static IAdvanticMeasureService _advanticMeasureService;
        private static IAdvanticSignalService _advanticSignalService;

        public AdvanticMeasureJobHelper(IConfigurationProvider configProvider, IAdvanticMeasureService advanticMeasureService, IAdvanticSignalService advanticSignalService)
        {
            _configProvider = configProvider;
            _advanticMeasureService = advanticMeasureService;
            _advanticSignalService = advanticSignalService;
        }

        public AdvanticMeasureJob GetJob() 
        {
            var result = new AdvanticMeasureJob();

            result.Name = "Advantic Measure Loader Job";
            result.JobDetail = createJobDetail();
            result.Trigger = createTrigger();

            return result;
        }

        private ITrigger createTrigger()
        {
            _logger.Info("Preparing  Advantic Measure Loader Trigger");
            var jobDataMap = getJobData();
            var cronExpression = _configProvider.GetMeasuresLoaderCronExpression();
            var result = getTrigger(jobDataMap, cronExpression);
            _logger.Info("File  Measures REE Loader Trigger prepared");
            return result;
        }

        private ITrigger getTrigger(JobDataMap jobDataMap, string cronExpression)
        {
            return TriggerBuilder.Create()
                .WithCronSchedule(cronExpression, x => x.WithMisfireHandlingInstructionDoNothing())
                .UsingJobData(jobDataMap)
                .Build();
        }

        private JobDataMap getJobData()
        {
            _logger.Info("Preparing Advantic Measure Loader Job Data");
            JobDataMap result = new JobDataMap();
            result.Put("ConfigurationProvider", _configProvider);
            result.Put("AdvanticMeasureService", _advanticMeasureService);
            result.Put("AdvanticSignalService", _advanticSignalService);
            _logger.Info("Done preparing Advantic Measure Loader Job Data");
            return result;
        }

        private IJobDetail createJobDetail()
        {
            _logger.Info("Preparing Advantic Measure Loader JobDetail");

            var result = JobBuilder.Create()
                .OfType(typeof(AdvanticMeasureJob))
                .Build();

            _logger.Info("JobDetail Advantic Measure Loader prepared");
            return result;
        }

    }
}
