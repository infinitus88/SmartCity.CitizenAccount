using AutoMapper;
using FluentAssertions;
using Moq;
using SmartCity.CitizenAccount.Api.Models.Citizens;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Maps;
using SmartCity.CitizenAccount.Services.CitizenAppService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace SmartCity.CitizenAccount.Services.Tests
{
    public class CitizenServiceTests
    {
        private Mock<IGenericRepository> _repository;
        private List<Citizen> _citizenList;
        private ICitizenService _service;
        private Random _random;
        private IMapper _mapper;

        public CitizenServiceTests()
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

            _service = new CitizenService(_repository.Object, _mapper);
        }

        [Fact]
        public void GetAllCitizensShouldReturnAll()
        {
            _citizenList.Add(new Citizen { FullName = "Rustam Minnikhanov" });

            var result = _service.GetAllCitizens().ToList();
            result.Count().Should().Be(1);
        }

        [Fact]
        public void GetCitizenByIdShouldReturnById()
        {
            var citizen = new Citizen {
                Id = Guid.NewGuid().ToString(),
                FullName = "Rustam Minnikhanov",
                DateOfBirth = DateTime.ParseExact("2011-03-21 13:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                RegistrationDate = DateTime.ParseExact("2011-03-21 13:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
            };
            _citizenList.Add(citizen);

            var result = _service.GetCitizenById(citizen.Id);
            result.Should().BeEquivalentTo(_mapper.Map<CitizenModel>(citizen));
        }
    }
}
