using MyEcoSpace.Logger.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Realizations
{
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
            foreach (var a in logBuffer)
            {
                SB.AppendLine(a.ToString());
            }
            File.AppendAllText($"{Path}/filename.log", SB.ToString());
        }
    }
}
