using System;
using NLog;

namespace MVCUI.Logger
{
    public class NLogAdaptor : ILogger
    {
        private static NLog.Logger logger;

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