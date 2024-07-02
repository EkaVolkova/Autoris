using Autoris.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Autoris.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        ILogger _logger;
        /// <summary>
        /// Конструктор класса UserController
        /// </summary>
        public UserController(ILogger logger)
        {
            _logger = logger;

            _logger.WriteEvent("Сообщение о событии в программе");
            _logger.WriteError("Сообщение об ошибки в программе");

        }

        [HttpGet]
        public User GetUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "Ivanov@yandex.ru",
                Login = "IIvanov",
                Password = "123456789"
            };
        }
    }
}
