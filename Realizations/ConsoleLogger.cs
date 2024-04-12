using MyEcoSpace.Logger.Configurations;
using MyEcoSpace.Logger.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Realizations
{
    internal class ConsoleLogger<T> : BaseLogger<T>
    {
        public ConsoleLogger(LoggerConfiguration config) : base(config)
        {

        }

        protected override async Task WriteBufferToStore()
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var a in logBuffer)
                {
                    Console.WriteLine(a.ToString());
                }
            });
        }
    }
}
