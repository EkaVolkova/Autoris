using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.Exstensions
{
    /// <summary>
    /// Класс исключений для добавления роли пользователя
    /// </summary>
    public class AddRoleExtensions : Exception
    {
        public AddRoleExtensions(string? message) : base(message)
        {
            
        }
    }
}
