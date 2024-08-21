using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.BLL.Exceptions
{
    /// <summary>
    /// Класс исключений для добавления роли пользователя
    /// </summary>
    public class AddRoleException : Exception
    {
        public AddRoleException(string? message) : base(message)
        {
            
        }
    }
}
