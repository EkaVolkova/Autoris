using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris
{
    /// <summary>
    /// Класс для логирования событий
    /// </summary>
    public class Logger : ILogger
    {
        private string saveDir;

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
        /// Логирует событие в файл
        /// </summary>
        /// <param name="eventMessage"></param>
        /// <returns></returns>
        private async Task WriteEventFileAsync(string eventMessage)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            string logMessage = $"[{DateTime.UtcNow}] Event: {eventMessage}\r\n";
            string logFilePath = Path.Combine(saveDir, "Event.txt");

            await File.AppendAllTextAsync(logFilePath, logMessage);
        }

        /// <summary>
        /// Логирует ошибку в файл
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private async Task WriteErrorFileAsync(string errorMessage)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            string logMessage = $"[{DateTime.UtcNow}] Error: {errorMessage}\r\n";
            string logFilePath = Path.Combine(saveDir, "Error.txt");

            await File.AppendAllTextAsync(logFilePath, logMessage);
        }



        /// <summary>
        /// Логирует событие
        /// </summary>
        /// <param name="eventMessage"></param>
        public async void WriteEvent(string eventMessage)
        {
            
            WriteEventConsole(eventMessage);
            await WriteEventFileAsync(eventMessage);
        }

        /// <summary>
        /// Логирует ошибку
        /// </summary>
        /// <param name="errorMessage"></param>
        public async void WriteError(string errorMessage)
        {
            WriteErrorConsole(errorMessage);
            await WriteErrorFileAsync(errorMessage);
        }



    }
}
