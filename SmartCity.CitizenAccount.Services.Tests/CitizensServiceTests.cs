using AutoMapper;
using FluentAssertions;
using Moq;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Citizens;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Maps;
using SmartCity.CitizenAccount.Services.CitizenAppService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SmartCity.CitizenAccount.Services.Tests
{
    public class CitizensServiceTests
    {
        private Mock<IGenericRepository> _repository;
        private List<Citizen> _citizenList;
        private ICitizensService _service;
        private Random _random;
        private IMapper _mapper;

        public CitizensServiceTests()
        {
            _random = new Random();
            _repository = new Mock<IGenericRepository>();

            _citizenList = new List<Citizen>();
            _repository.Setup(x => x.Query<Citizen>()).Returns(() => _citizenList.AsQueryable());

            // Set up mapper configuration
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = new Mapper(config);

            _service = new CitizensService(_repository.Object, _mapper);
        }

        [Fact]
        public void GetAllCitizensShouldReturnAll()
        {
            _citizenList.Add(new Citizen { Id = Guid.NewGuid().ToString() });

            var result = _service.Get().ToList();
            result.Count().Should().Be(1);
        }

        [Fact]
        public void GetCitizenByIdShouldReturnItemById()
        {
            var citizen = new Citizen {
                Id = Guid.NewGuid().ToString()
            };
            _citizenList.Add(citizen);

            var result = _service.Get(citizen.Id);
            result.Should().BeEquivalentTo(citizen);
        }

        [Fact]
        public async Task CreateShouldSaveNew()
        {
            var model = new CreateCitizenModel
            {
                FullName = "Rustam Minnikhanov",
                DateOfBirth = DateTime.Now
            };

            var result = await _service.Create(model);

            result.FullName.Should().Be(model.FullName);
            result.DateOfBirth.Should().Be(model.DateOfBirth);
        }


        [Fact]
        public void GetCitizenByIdShouldThrowExceptionIfItemIsNotFoundById()
        {
            var citizen = new Citizen { Id = Guid.NewGuid().ToString() };
            _citizenList.Add(citizen);

            Action get = () =>
            {
                _service.Get(Guid.NewGuid().ToString());
            };

            get.Should().Throw<NotFoundException>();
        }
    }
}
