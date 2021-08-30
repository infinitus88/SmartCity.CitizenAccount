using AutoMapper;
using SmartCity.CitizenAccount.Api.Models.Citizens;
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
        }

        private void CitizenMappings()
        {
            CreateMap<Citizen, CitizenModel>();
            CreateMap<RegisterCitizenModel, Citizen>();
        }
    }
}
