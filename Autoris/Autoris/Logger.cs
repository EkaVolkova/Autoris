using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris
{
    /// <summary>
    /// Класс для логирования событий
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Логирует событие
        /// </summary>
        /// <param name="eventMessage"></param>
        public void WriteEvent(string eventMessage)
        {
            Console.WriteLine($"[{DateTime.UtcNow}] Event: {eventMessage}");
        }

        /// <summary>
        /// Логирует ошибку
        /// </summary>
        /// <param name="errorMessage"></param>
        public void WriteError(string errorMessage)
        {
            Console.WriteLine($"[{DateTime.UtcNow}] Error: {errorMessage}");
        }



    }
}
