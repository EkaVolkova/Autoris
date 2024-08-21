using Autoris.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        UserContext _context;
        public RoleRepository(UserContext context)
        {
            _context = context;
        }

        public async Task AddRole(Role role)
        {
            // Добавление пользователя
            var entry = _context.Entry(role);
            if (entry.State == EntityState.Detached)
                await _context.Roles.AddAsync(role);

            // Сохранение изенений
            await _context.SaveChangesAsync();

        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles;
        }

        public Role GetRole(string name)
        {

            return _context.Roles.Where(r => r.Name == name).ToList().FirstOrDefault();
        }
    }
}
