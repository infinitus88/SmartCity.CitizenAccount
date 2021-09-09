using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Citizens;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Services.CitizenAppService
{
    public class CitizensService : ICitizensService
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;


        public CitizensService(IGenericRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private IQueryable<Citizen> GetQuery()
        {
            var query = _repository.Query<Citizen>();
            return query;
        }

        public IQueryable<Citizen> Get()
        {
            var citizens = GetQuery();

            return citizens;
        }

        public Citizen Get(string id)
        {
            var citizen = GetQuery().Where(c => c.Id == id).FirstOrDefault();

            if (citizen == null)
            {
                throw new NotFoundException("Citizen is not found");
            }

            return citizen;
        }

        public async Task<Citizen> Create(CreateCitizenModel input)
        {
            var citizen = _mapper.Map<Citizen>(input);
            _repository.Add<Citizen>(citizen);

            await _repository.SaveAsync();

            return citizen;
        }
    }
}
