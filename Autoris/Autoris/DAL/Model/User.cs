﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.DAL.Model
{
    /// <summary>
    /// Класс описывающий сущность пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID в БД
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// E-mail адрес пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Навигационное свойство id роли пользователя
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public Role Role { get; set; }
    }
}
