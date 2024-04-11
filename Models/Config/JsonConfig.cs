using MyEcoSpace.Logger.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Models.Config
{
    public class JsonConfig
    {
        public ChainResponsibilityMethod LogginingType { get; set; }
        public GetMethodType GetMethodType { get; set; }
        public List<DBLoggerConfiguration> Loggers { get; set; }
    }
}
