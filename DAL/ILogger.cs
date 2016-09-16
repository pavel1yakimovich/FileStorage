using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface ILogger
    {
        void Error(string msg);
        void Error(Exception e, string msg);
        void Info(string msg);
    }
}
