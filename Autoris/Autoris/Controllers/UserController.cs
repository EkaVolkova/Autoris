using Autoris.Models;
using Autoris.ViewModels;
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

            _logger.WriteEvent(new Event("Сообщение о событии в программе"));
            _logger.WriteError(new Error("Сообщение об ошибке в программе"));

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

        [HttpGet]
        [Route("viewmodel")]
        public UserViewModel GetUserViewModel()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "ivan@gmail.com",
                Password = "11111122222qq",
                Login = "ivanov"
            };

            UserViewModel userViewModel = new UserViewModel(user);

            return userViewModel;
        }
    }
}
