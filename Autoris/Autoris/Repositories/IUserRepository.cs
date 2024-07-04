using Autoris.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Получить пользователя по его логину
        /// </summary>
        /// <param name="login">логин пользователя</param>
        /// <returns>Объект класса User</returns>
        User GetByLogin(string login);

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        Task AddUser(User user);
    }
}
