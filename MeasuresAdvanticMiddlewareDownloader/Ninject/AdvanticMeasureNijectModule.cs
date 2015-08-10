using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Ninject;
using MeasuresAdvanticMiddlewareDownloader.Quartz;
using MeasuresAdvanticMiddlewareDownloader.Configuration;
using MeasuresAdvanticMiddleware.Services;

namespace MeasuresAdvanticMiddlewareDownloader.Ninject
{
    public class AdvanticMeasureNijectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICronDispatcher>().To<Dispatcher>();
            Bind<IAdvanticSignalService>().To<AdvanticsSignalService>();
            Bind<IAdvanticMeasureService>().To<AdvanticMeasureService>();
            Bind<IAdvanticMeasureJobFactory>().To<AdvanticMeasureJobFactory>();
            Bind<IConfigurationProvider>().To<ConfigurationProvider>();
        }

        public static T GetDependency<T>()
        {
            return new StandardKernel(new AdvanticMeasureNijectModule()).Get<T>();
        }
    }
}
