using Autoris.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.Repositories
{
    public class UserRepository : IUserRepository
    {
        UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(p => p.Role).ToList();
        }

        /// <summary>
        /// Получить пользователя по его логину
        /// </summary>
        /// <param name="login">логин пользователя</param>
        /// <returns>Объект класса User</returns>
        public User GetByLogin(string login)
        {
            var users = GetAll();
            var user = users.Where(u => u.Login == login).ToList().FirstOrDefault();
            return user;


        }


        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        public async Task AddUser(User user)
        {
            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }
    }
}
