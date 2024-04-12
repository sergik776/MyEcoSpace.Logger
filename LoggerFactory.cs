using MyEcoSpace.Logger.Interfaces;
using MyEcoSpace.Logger.Realizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger
{
    public class LoggerFactory
    {
        public ILogger<T> Create<T>() where T : class, new()
        {
            return new MainLogger<T>();
        }
    }
}
