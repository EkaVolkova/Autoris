using AutoMapper;
using Autoris.DAL.Model;
using Autoris.PLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.PLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Создаем способ получения UserViewModel на основе User
            CreateMap<User, UserViewModel>()
                //Для передачи объекта User в качестве параметра для конструктора UserViewModel
                .ConstructUsing(v => new UserViewModel(v));
        }
    }
}
