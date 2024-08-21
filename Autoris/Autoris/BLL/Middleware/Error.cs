using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.BLL.Middleware
{
    public class Error
    {
        /// <summary>
        /// Время возникновения ошибки
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        public string Message { get; set; }

        public Error(string message)
        {
            Message = message;
            DateTime = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"[{DateTime}] Error: {Message}\r\n";
        }

    }
}
