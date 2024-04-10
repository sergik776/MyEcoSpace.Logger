using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcoSpace.Logger.Interfaces
{
    /// <summary>
    /// Интерфейс отправки экстренного уведомления
    /// </summary>
    public interface IEmergencyNotification
    {
        /// <summary>
        /// Отправить уведомление
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns></returns>
        public string SendAlarm(string message);
    }
}
