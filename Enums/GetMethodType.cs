using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Enums
{
    /// <summary>
    /// Способ получения названия метода для лога
    /// </summary>
    public enum GetMethodType : byte
    {
        /// <summary>
        /// название метода указывает разработчик передавая в лог
        /// </summary>
        HardCode,
        /// <summary>
        /// Название метода вытягивается из стек трейса
        /// </summary>
        StackTrace,
        /// <summary>
        /// Название метода вытягивается из рефлексии
        /// </summary>
        Reflection
    }
}
