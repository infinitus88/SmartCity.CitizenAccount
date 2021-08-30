using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Citizens;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Linq;

namespace SmartCity.CitizenAccount.Services.CitizenAppService
{
    public class CitizenService : ICitizenService
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;


        public CitizenService(IGenericRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private IQueryable<Citizen> GetQuery()
        {
            var query = _repository.Query<Citizen>();
            return query;
        }

        public IQueryable<CitizenModel> GetAllCitizens()
        {
            var citizens = GetQuery();

            return citizens.ProjectTo<CitizenModel>(_mapper.ConfigurationProvider);
        }

        public CitizenModel GetCitizenById(string id)
        {
            var citizen = GetQuery().Where(c => c.Id == id).FirstOrDefault();

            if (citizen == null)
            {
                throw new NotFoundException("Citizen is not found");
            }

            return _mapper.Map<CitizenModel>(citizen);
        }

        public CitizenModel RegisterCitizen(RegisterCitizenModel input)
        {
            var citizen = _mapper.Map<Citizen>(input);
            _repository.Add<Citizen>(citizen);

            return _mapper.Map<CitizenModel>(citizen);
        }
    }
}
