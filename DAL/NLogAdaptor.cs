using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DAL
{
    public class NLogAdaptor : ILogger
    {
        private Logger logger;

        public NLogAdaptor()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        public void Error(string msg)
        {
            this.logger.Error(msg);
        }

        public void Error(Exception e, string msg)
        {
            logger.Error(e, msg);
        }

        public void Info(string msg)
        {
            this.logger.Info(msg);
        }
    }
}
