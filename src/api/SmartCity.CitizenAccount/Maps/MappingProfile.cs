using AutoMapper;
using SmartCity.CitizenAccount.Api.Models.Citizens;
using SmartCity.CitizenAccount.Api.Models.Emails;
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
            EmailMappings();
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
            CreateMap<CreateUserModel, User>().AfterMap<TrimAllStringProperties>();

            // Auth
            CreateMap<UserWithToken, UserWithTokenModel>();
        }

        private void EmailMappings()
        {
            CreateMap<Email, EmailModel>();
            CreateMap<CreateEmailModel, Email>();
        }
    }

    internal class PrepareEmails : IMappingAction<object, object>
    {
        public void Process(object source, object destination, ResolutionContext context)
        {
            
        }
    }

    internal class TrimAllStringProperties : IMappingAction<object, object>
    {
        public void Process(object source, object destination, ResolutionContext context)
        {
            var stringProperties = destination.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(destination, null);
                if (currentValue != null)
                {
                    stringProperty.SetValue(destination, currentValue.Trim(), null);
                }
            }
        }
    }
}
