using AutoMapper;
using FluentAssertions;
using Moq;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Maps;
using SmartCity.CitizenAccount.Services.UserAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartCity.CitizenAccount.Services.Tests
{
    public class UsersServiceTests
    {
        private Mock<IGenericRepository> _repository;
        private List<User> _usersList;
        private IUsersService _service;
        private Random _random;
        private IMapper _mapper;

        public UsersServiceTests()
        {
            _repository = new Mock<IGenericRepository>();

            _usersList = new List<User>();
            _repository.Setup(x => x.Query<User>()).Returns(() => _usersList.AsQueryable());

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = new Mapper(config);

            _service = new UsersService(_repository.Object, _mapper);
            _random = new Random();
        }

        [Fact]
        public void GetShouldReturnAll()
        {
            _usersList.Add(new User());

            var result = _service.Get();
            result.Count().Should().Be(1);
        }

        [Fact]
        public void GetShouldReturnAllExceptDeleted()
        {
            _usersList.Add(new User());
            _usersList.Add(new User { IsDeleted = true });
            _usersList.Add(new User { IsDeleted = true });

            var result = _service.Get();
            result.Count().Should().Be(1);
        }

        [Fact]
        public void GetShouldReturnUserById()
        {
            var user = new User { Id = _random.Next() };
            _usersList.Add(user);

            var result = _service.Get(user.Id);
            //result.Should().BeEquivalentTo(user);
            result.Should().Be(user);
        }

        [Fact]
        public void GetShouldThrowExceptionIfUserIsNotFoundById()
        {
            var user = new User { Id = _random.Next() };
            _usersList.Add(user);

            Action get = () =>
            {
                _service.Get(_random.Next());
            };

            get.Should().Throw<NotFoundException>();
        }


        [Fact]
        public void GetShouldThrowExceptionIfUserIsDeleted()
        {
            var user = new User { Id = _random.Next(), IsDeleted = true };
            _usersList.Add(user);

            Action get = () =>
            {
                _service.Get(user.Id);
            };

            get.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task CreateShouldSaveNewUser()
        {
            var model = new CreateUserModel()
            {
                Password = _random.Next().ToString(),
                Email = _random.Next().ToString(),
                DisplayName = _random.Next().ToString()
            };

            var result = await _service.Create(model);

            result.Password.Should().NotBeEmpty();
            result.Email.Should().Be(model.Email);
            result.DisplayName.Should().Be(model.DisplayName);
            result.Role.Should().Be("user");
        }

        [Fact]
        public void CreateShouldThrowExceptionIfEmailIsNotUnique()
        {
            var model = new CreateUserModel
            {
                Email = _random.Next().ToString()
            };

            _usersList.Add(new User { Email = model.Email });

            Action create = () =>
            {
                var x = _service.Create(model).Result;
            };

            create.Should().Throw<BadRequestException>();
        }


        [Fact]
        public async Task UpdateShouldUpdateUserFields()
        {
            var user = new User { Id = _random.Next(), Status = Status.Active };
            _usersList.Add(user);

            var model = new UpdateUserModel
            {
                Email = _random.Next().ToString(),
                DisplayName = _random.Next().ToString(),
                PhotoUrl = _random.Next().ToString(),
            };

            var result = await _service.Update(user.Id, model);

            result.Should().Be(user);
            result.Email.Should().Be(model.Email);
            result.DisplayName.Should().Be(model.DisplayName);
            result.PhotoUrl.Should().Be(model.PhotoUrl);
        }

        [Fact]
        public async Task DeleteShouldMarkUserAsDeleted()
        {
            var user = new User { Id = _random.Next() };
            _usersList.Add(user);

            await _service.Delete(user.Id);

            user.IsDeleted.Should().BeTrue();
        }
        
        [Fact]
        public void DeleteShouldThrowExceptionIfUserIsNotFound()
        {
            Action create = () =>
            {
                _service.Delete(_random.Next()).Wait();
            };

            create.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task ChangePasswordShouldChangeUsersPassword()
        {
            var user = new User { Id = _random.Next() };
            _usersList.Add(user);

            var newPassword = _random.Next().ToString();

            await _service.ChangePassword(user.Id, new ChangeUserPasswordModel
            {
                Password = newPassword
            });

            user.Password.Should().NotBeEmpty();
        }
    }
}
