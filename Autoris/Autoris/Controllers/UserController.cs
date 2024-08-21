using AutoMapper;
using Autoris.Exceptions;
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
    [ServiceFilter(typeof(ExceptionHandler))]
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

        }


        /// <summary>
        /// Добавление роли
        /// </summary>
        /// <param name="name"></param>
        [HttpPost]
        [Route("Roles")]
        public async void AddRole(string name)
        {
            if(_roleRepository.GetRole(name) != null)
                throw new Exception("Роль с таким именем уже существует");


            var role = new Role { Name = name };
            await _roleRepository.AddRole(role);
            _logger.WriteEvent(new Event($"Добавлена роль пользователя: {role}"));
        }

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="name">имя</param>
        /// <param name="lastName">фамилия</param>
        /// <param name="login">логин</param>
        /// <param name="password">пароль</param>
        /// <param name="email">Email</param>
        /// <param name="roleName">Название роли</param>
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
            _logger.WriteEvent(new Event($"Добавлен пользователь:\r\nИмя: {name}\r\nФамилия: {lastName}\r\nЛогин: {login}"));


        }

        /// <summary>
        /// Аутентификация
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
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
            _logger.WriteEvent(new Event($"Аутентификация пользователя {login} прошла успешно"));

            //Возвращаем ViewModel пользователя
            return _mapper.Map<UserViewModel>(user);

        }

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        /// <summary>
        /// Получение пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("user")]
        public UserViewModel GetUser(string login)
        {
            var users = _userRepository.GetAll();
            var user = users.Where(u => u.Login == login).FirstOrDefault();
            if (user == null)
                throw new ArgumentException($"Пользователь {login} не найден");

            var userViewModel = _mapper.Map<UserViewModel>(user);
            _logger.WriteEvent(new Event($"Пользователь {login} найден"));
            return userViewModel;
        }

    }
}
