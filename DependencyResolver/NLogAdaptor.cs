using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DependencyResolver
{
    public class NLogAdaptor : ILogger
    {
        private static Logger logger;

        public NLogAdaptor()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Error(string msg)
        {
            logger.Error(msg);
        }

        public void Error(Exception e, string msg)
        {
            logger.Error(e, msg);
        }

        public void Info(string msg)
        {
            logger.Info(msg);
        }
    }
}
