using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeasuresAdvanticMiddlewareDownloader.Configuration;
using MeasuresAdvanticMiddlewareDownloader.Ninject;
using MeasuresAdvanticMiddleware.Services;

namespace MeasuresAdvanticMiddlewareDownloader.Quartz
{

    public interface IAdvanticMeasureJobFactory
    {
        IList<AdvanticMeasureJob> GetAdvanticMeasureJobs();
    }

    class AdvanticMeasureJobFactory : IAdvanticMeasureJobFactory
    {
        private static IConfigurationProvider _configProvider;
        private static IAdvanticMeasureService _advanticMeasureService;
        private static IAdvanticSignalService _advanticSignalService;

        public AdvanticMeasureJobFactory(IConfigurationProvider configProvider, IAdvanticMeasureService advanticMeasureService, IAdvanticSignalService advanticSignalService)
        {
            _configProvider = configProvider;
            _advanticMeasureService = advanticMeasureService;
            _advanticSignalService = advanticSignalService;
        }

        public IList<AdvanticMeasureJob> GetAdvanticMeasureJobs()
        {
            var result = new List<AdvanticMeasureJob>();

            foreach (var jobHelper in getJobHelpers())
            {
                var job = jobHelper.GetJob();
                result.Add(job);
            }

            return result;
        }

        private IList<IAdvanticMeasureJobHelper> getJobHelpers()
        {
            var result = new List<IAdvanticMeasureJobHelper>();
            result.Add(AdvanticMeasureNijectModule.GetDependency<AdvanticMeasureJobHelper>());
            return result;
        }
    }
}
