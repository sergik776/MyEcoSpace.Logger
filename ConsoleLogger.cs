using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger
{
    public class ConsoleLogger<T> : BaseLogger<T>
    {
        public ConsoleLogger(LoggerConfiguration config) : base(config)
        {

        }

        protected override async Task WriteBufferToStore()
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var a in base.logBuffer)
                {
                    Console.WriteLine(a.ToString());
                }
            });
        }
    }

    internal class FileLogger<T> : BaseLogger<T>
    {
        string Path;
        public FileLogger(FileLoggerConfiguration config) : base(config)
        {
            Path = config.SaveFilePath;
        }

        protected override async Task WriteBufferToStore()
        {

                StringBuilder SB = new StringBuilder();
                foreach (var a in base.logBuffer)
                {
                    SB.AppendLine(a.ToString());
                }
                File.AppendAllText($"{Path}/filename.log", SB.ToString());
        }
    }
}
