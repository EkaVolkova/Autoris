﻿using AutoMapper;
using Autoris.Exstensions;
using Autoris.Models;
using Autoris.Models.Db;
using Autoris.Repositories;
using Autoris.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Autoris.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// Конструктор класса UserController
        /// </summary>
        public UserController(ILogger logger, IMapper mapper, IUserRepository  userRepository, IRoleRepository roleRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _logger.WriteEvent(new Event("Сообщение о событии в программе"));
            _logger.WriteError(new Error("Сообщение об ошибке в программе"));

        }

        [HttpPost]
        [Route("Roles")]
        public async void AddRole(string name)
        {
            if(_roleRepository.GetRole(name) != null)
                throw new AddRoleExtensions("Роль с таким именем уже существует");


            var role = new Role { Name = name };
            await _roleRepository.AddRole(role);
        }

        [HttpPost]
        public async void AddUser(string name, string lastName, string login, string password, string email, string roleName)
        {
            var role = _roleRepository.GetRole(roleName);
            if (role == null)
            {
                throw new ArgumentException("Роли с таким именем не существует");
            }

            

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = name,
                LastName = lastName,
                Login = login,
                Password = password,
                Email = email,
                Role = role
            };

            await _userRepository.AddUser(user);


        }
        
        [HttpPost]
        [Route("authenticate")]
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            //Проверка на пустые входные значения
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException("Имя пользователя не введено");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Пароль не введен");

            //Получаем пользователя по логину
            User user = _userRepository.GetByLogin(login);


            //проверяем, есть ли пользователь
            if (user is null)
                throw new AuthenticationException("Пользователь на найден");

            //Проверяем пароль
            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            //Инициализируем список утверждений проверки подлинности
            var claims = new List<Claim>()
            {
                //Добавляем одно утверждение с типом по умолчанию и логином пользователя
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                //Добавляем одно утверждение с типом роли по умолчанию и ролью
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            //Создали объект класса ClaimsIdentity, который реализует интерфейс IIdentity и представляет текущего пользователя
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AppCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            //Добавляем в контекст ClaimsPrincipal для работы с авторизацией, который содержит ClaimsIdentity
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            //Возвращаем ViewModel пользователя
            return _mapper.Map<UserViewModel>(user);

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

        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("viewmodel")]
        public UserViewModel GetUserViewModel()
        {
            var _roles = _roleRepository.GetAll();
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "ivan@gmail.com",
                Password = "11111122222qq",
                Login = "ivanov",
                Role = _roles.FirstOrDefault()
            };

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }
    }
}
