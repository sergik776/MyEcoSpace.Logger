using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Exceptions
{
    internal class ConfigLoadException : Exception
    {
        public ConfigLoadException(string pn, string pv) : base($"Ошибка при чтении конфигураций, свойство: {pn} не соответствует сигнатуре типа {pv}")
        {
            PropertyName = pn;
            PropertyValue = pv;
        }

        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}
