using AutoMapper;
using SmartCity.CitizenAccount.Api.Models.Citizens;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Data.Access.Common;
using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CitizenMappings();
            UserMappings();
        }

        private void CitizenMappings()
        {
            CreateMap<Citizen, CitizenModel>();
            CreateMap<CreateCitizenModel, Citizen>();
        }

        private void UserMappings()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.UId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PhotoURL, opt => opt.MapFrom(src => src.PhotoUrl));
            CreateMap<CreateUserModel, User>();

            CreateMap<UserWithToken, UserWithTokenModel>();
        }
    }
}
