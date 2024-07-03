using AutoMapper;
using Autoris.Models;
using Autoris.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoris.Mapping
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
