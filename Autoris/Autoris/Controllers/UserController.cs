using Autoris.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Autoris.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Конструктор класса UserController
        /// </summary>
        public UserController()
        {
            var logger = new Logger();

            logger.WriteEvent("Сообщение о событии в программе");
            logger.WriteError("Сообщение об ошибки в программе");

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
