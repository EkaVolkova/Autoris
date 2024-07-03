using Autoris.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Autoris
{
    /// <summary>
    /// Класс для логирования событий
    /// </summary>
    public class Logger : ILogger
    {
        private readonly string saveDir;
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// Конструктор класса логирования
        /// </summary>
        public Logger()
        {
            //Создает новую директорию при каждом запуске
            saveDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs", DateTime.UtcNow.ToString("dd MM yyyy HH.mm.ss"));
            if (!Directory.Exists(saveDir))
                Directory.CreateDirectory(saveDir);

        }

        /// <summary>
        /// Логирует ошибку в консоль
        /// </summary>
        private void WriteErrorConsole(Error eventMessage)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Error");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"\t{eventMessage}");
        }

        /// <summary>
        /// Логирует событие в консоль
        /// </summary>
        /// <param name="eventMessage"></param>
        private void WriteEventConsole(Event eventMessage)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Event");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"\t{eventMessage}");
        }

        /// <summary>
        /// Логирует событие в файл
        /// </summary>
        /// <param name="eventMessage"></param>
        /// <returns></returns>
        private void WriteEventFileAsync(Event eventMessage)
        {
            _lock.EnterWriteLock();
            string logFilePath = Path.Combine(saveDir, "Event.txt");
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
                {
                    writer.WriteLine(eventMessage);

                }
            }
            finally
            {
                _lock.ExitWriteLock();

            }


        }

        /// <summary>
        /// Логирует ошибку в файл
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private void WriteErrorFileAsync(Error errorMessage)
        {
            _lock.EnterWriteLock();
            string logFilePath = Path.Combine(saveDir, "Error.txt");
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
                {
                    writer.WriteLine(errorMessage);

                }
            }
            finally
            {
                _lock.ExitWriteLock();

            }

        }



        /// <summary>
        /// Логирует событие
        /// </summary>
        /// <param name="eventMessage"></param>
        public void WriteEvent(Event eventMessage)
        {
            
            WriteEventConsole(eventMessage);
            WriteEventFileAsync(eventMessage);
        }

        /// <summary>
        /// Логирует ошибку
        /// </summary>
        /// <param name="errorMessage"></param>
        public void WriteError(Error errorMessage)
        {
            WriteErrorConsole(errorMessage);
            WriteErrorFileAsync(errorMessage);
        }



    }
}
