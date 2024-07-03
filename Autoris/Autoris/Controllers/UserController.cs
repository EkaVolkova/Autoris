using AutoMapper;
using Autoris.Models;
using Autoris.Models.Db;
using Autoris.Repositories;
using Autoris.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Autoris.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор класса UserController
        /// </summary>
        public UserController(ILogger logger, IMapper mapper, IUserRepository  userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;

            _logger.WriteEvent(new Event("Сообщение о событии в программе"));
            _logger.WriteError(new Error("Сообщение об ошибке в программе"));

        }


        [HttpPost]
        public void AddUser(string name, string lastName, string login, string password, string email)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = name,
                LastName = lastName,
                Login = login,
                Password = password,
                Email = email
            };

            _userRepository.AddUser(user);


        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        [HttpGet]
        [Route("user")]
        public User GetUser(string login)
        {
            return _userRepository.GetByLogin(login);
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

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }
    }
}
