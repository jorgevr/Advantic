using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace MeasuresAdvanticMiddleware
{
    public class MeasureAdvanticStartJob : IJob 
    {
        #region private Properties

        //private ILogger _logger = NullLogger.Instance;
        private MeasureAdvanticStart _measureAdvanticStart;
       
        private string _baseDirectory;
        #endregion

        #region Public Properties

        //public ILogger Logger
        //{
        //    get { return _logger; }
        //    set { _logger = value; }
        //}

        public string BaseDirectory
        {
            get { return _baseDirectory; }
            set { _baseDirectory = value; }
        }

        

        #endregion

        #region IJob Method

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                //var log = context.JobDetail.JobDataMap.Get("Logger") as ILogger;
                //if (log != null)
                //    Logger = log;

                _measureAdvanticStart = new MeasureAdvanticStart();
                _measureAdvanticStart.StartExecution();
            }
            catch (Exception ex)
            {
                //Logger.Error("Exception ", ex);
            }
        }

        #endregion
    }
}
