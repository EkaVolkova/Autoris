using Autoris.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.Exceptions
{
    /// <summary>
    /// Класс фильтра исключений
    /// </summary>
    public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Логер
        /// </summary>
        ILogger _logger;
        public ExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Реакция на событие возникновение исключений
        /// </summary>
        /// <param name="context">Текущий контекст</param>
        public void OnException(ExceptionContext context)
        {
            string message = $"Произошла ошибка, администратор сайта уже спешит ее исправить\r\n{context.Exception.Message}";
            context.Result = new BadRequestObjectResult(message);
            _logger.WriteError(new Error(message));
        }
    }
}
