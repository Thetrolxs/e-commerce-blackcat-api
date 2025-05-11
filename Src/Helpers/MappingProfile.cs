using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using e_commerce_blackcat_api.Src.Dtos;
using e_commerce_blackcat_api.Src.Models;

namespace e_commerce_blackcat_api.Src.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}