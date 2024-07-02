﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.Models
{
    public class Event
    {
        /// <summary>
        /// Время возникновения события
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Сообщение события
        /// </summary>
        public string Message { get; set; }

        public Event(string message)
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
