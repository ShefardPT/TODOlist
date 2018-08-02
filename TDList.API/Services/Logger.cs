using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TDList.API.Services
{
    public class Logger: NLog.Logger
    {
        private static NLog.Logger _logger;
        static Logger()
        {
            _logger = NLog.LogManager.GetLogger("TODOListLogger");
        }
        public Logger GetLogger()
        {
            return _logger;
        }
    }
}
