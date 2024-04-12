using MyEcoSpace.Logger.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Models.Config
{
    public class JsonConfig
    {
        public JsonConfig() 
        {
            Loggers = new List<LoggerConfiguration>();
        }

        public ChainResponsibilityMethod LogginingType { get; set; }
        public GetMethodType GetMethodType { get; set; }
        public List<LoggerConfiguration> Loggers { get; set; }
    }
}
