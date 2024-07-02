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
        /// Логирует ошибку в консоль
        /// </summary>
        private void WriteErrorConsole(string eventMessage)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Error");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"\t[{DateTime.UtcNow}] {eventMessage}");
        }

        /// <summary>
        /// Логирует событие в консоль
        /// </summary>
        /// <param name="eventMessage"></param>
        private void WriteEventConsole(string eventMessage)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Event");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"\t[{DateTime.UtcNow}] {eventMessage}");
        }

        /// <summary>
        /// Логирует событие
        /// </summary>
        /// <param name="eventMessage"></param>
        public void WriteEvent(string eventMessage)
        {

            WriteEventConsole(eventMessage);
        }

        /// <summary>
        /// Логирует ошибку
        /// </summary>
        /// <param name="errorMessage"></param>
        public void WriteError(string errorMessage)
        {
            WriteErrorConsole(errorMessage);
        }



    }
}
