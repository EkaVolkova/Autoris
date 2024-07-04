using Autoris.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.Repositories
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Добавить роль
        /// </summary>
        /// <param name="role">Модель пользователя</param>
        Task AddRole(Role role);

        /// <summary>
        /// Получить список всех ролей
        /// </summary>
        /// <returns></returns>
        IEnumerable<Role> GetAll();

        Role GetRole(string name);

    }
}
