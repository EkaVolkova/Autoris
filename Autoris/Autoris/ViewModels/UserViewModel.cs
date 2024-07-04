using Autoris.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Autoris.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Полное имя в формате: [Имя] [Фамилия]
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Является ли домен российским
        /// </summary>
        public bool DomenFromRussia { get; set; }

        public string Role { get; set; }

        /// <summary>
        /// Конструктор для преобразования модели User в модель UserViewModel
        /// </summary>
        /// <param name="user">объект класса User</param>
        public UserViewModel(User user)
        {
            Id = user.Id;
            FullName = GetFullName(user.FirstName, user.LastName);
            DomenFromRussia = GetFromRussiaValue(user.Email);
            Role = user.Role.Name;


        }

        /// <summary>
        /// Конкатенация имени и фамилии в полное имя
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <returns>Полное имя в формате: [Имя] [Фамилия]</returns>
        private string GetFullName(string firstName, string lastName)
        {
            return String.Concat(firstName, " ", lastName);
        }

        /// <summary>
        /// Получает информацию о том, является ли домен Email российским
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>true - российских домен, false - не российский домен</returns>
        private bool GetFromRussiaValue(string email)
        {
            MailAddress mailAddress = new MailAddress(email);

            if (mailAddress.Host.Contains(".ru"))
                return true;
            return false;
        }

    }
}
