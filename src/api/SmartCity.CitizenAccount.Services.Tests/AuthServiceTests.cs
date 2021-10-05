using FluentAssertions;
using Moq;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Auth;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Access.Helpers;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Security;
using SmartCity.CitizenAccount.Security.Auth;
using SmartCity.CitizenAccount.Services.AuthAppService;
using SmartCity.CitizenAccount.Services.UserAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartCity.CitizenAccount.Services.Tests
{
    public class AuthServiceTests
    {
        private Mock<IGenericRepository> _repository;
        private List<User> _userList;
        private IAuthService _service;
        private Random _random;
        private Mock<ITokenBuilder> _tokenBuilder;
        private Mock<IUsersService> _userService;
        private Mock<ISecurityContext> _context;

        public AuthServiceTests()
        {
            _random = new Random();
            _repository = new Mock<IGenericRepository>();

            _userList = new List<User>();
            _repository.Setup(x => x.Query<User>()).Returns(() => _userList.AsQueryable());

            _tokenBuilder = new Mock<ITokenBuilder>(MockBehavior.Strict);
            _userService = new Mock<IUsersService>();

            _context = new Mock<ISecurityContext>(MockBehavior.Strict);
            _userService = new Mock<IUsersService>();

            _service = new AuthService(_repository.Object, _tokenBuilder.Object, _userService.Object, _context.Object);
        }

        [Fact]
        public void AuthenticateShouldReturnUserAndToken()
        {
            var password = _random.Next().ToString();
            var user = new User
            {
                Email = _random.Next().ToString(),
                Password = password.WithBCrypt(),
                Role = _random.Next().ToString(),
                Status = Status.Active
            };

            _userList.Add(user);

            var expireTokenDate = DateTime.Now + TokenAuthOption.ExpiresSpan;

            var token = _random.Next().ToString();
            _tokenBuilder.Setup(x => x.Build(
                user.Email,
                user.Role,
                It.Is<DateTime>(d => d - expireTokenDate < TimeSpan.FromSeconds(1)))
            ).Returns(token);

            var result = _service.Authenticate(user.Email, password);

            result.UserData.Should().Be(user);
            result.AccessToken.Should().Be(token);
        }

        [Fact]
        public void AuthenticateShouldThrowIfUserPasswordIsWrong()
        {
            var password = _random.Next().ToString();
            var user = new User
            {
                Email = _random.Next().ToString(),
                Password = password.WithBCrypt(),
                Status = Status.Active
            };
            _userList.Add(user);

            Action execute = () => _service.Authenticate(user.Email, _random.Next().ToString());

            execute.Should().Throw<BadRequestException>();
        }

        [Fact]
        public void AuthenticateShouldThrowIfUserIsDeleted()
        {
            var password = _random.Next().ToString();
            var user = new User
            {
                Email = _random.Next().ToString(),
                Password = password.WithBCrypt(),
                Status = Status.Active,
                IsDeleted = true
            };
            _userList.Add(user);

            Action execute = () => _service.Authenticate(user.Email, password);

            execute.Should().Throw<BadRequestException>();
        }

        [Fact]
        public async Task RegisterShouldCreateUserViaQuery()
        {
            var requestModel = new RegisterModel
            {
                Password = _random.Next().ToString(),
                Email = _random.Next().ToString(),
                DisplayName = _random.Next().ToString()
            };

            var createdUser = new User();

            _userService.Setup(x => x.Create(It.Is<CreateUserModel>(m =>
                m.DisplayName == requestModel.DisplayName
                && m.Password == requestModel.Password
                && m.Email == requestModel.Email
             ))).Returns(Task.FromResult(createdUser));

            var result = await _service.Register(requestModel);

            result.Should().Be(createdUser);
        }

        [Fact]
        public async Task ChangePasswordShouldCallUserQueryWithCurrentUser()
        {
            var user = new User { Id = _random.Next() };

            _context.SetupGet(x => x.User).Returns(user);

            var requestModel = new ChangeUserPasswordModel
            {
                Password = _random.Next().ToString()
            };

            _userService.Setup(x => x.ChangePassword(user.Id, requestModel))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await _service.ChangePassword(requestModel);

            _userService.Verify();
        }

        [Fact]
        public void RefreshTokenShouldReturnUserAndToken()
        {
            var user = new User
            {
                Id = _random.Next(),
                Email = _random.Next().ToString(),
                Role = _random.Next().ToString(),
                Status = Status.Active
            };

            _userList.Add(user);
            var oldToken = _random.Next().ToString();
            _tokenBuilder.Setup(x => x.GetUniqueNameByToken(oldToken)
            ).Returns(user.Email);

            var expireTokenDate = DateTime.Now + TokenAuthOption.ExpiresSpan;

            var token = _random.Next().ToString();
            _tokenBuilder.Setup(x => x.Build(
                user.Email,
                user.Role,
                It.Is<DateTime>(d => d - expireTokenDate < TimeSpan.FromSeconds(1)))
            ).Returns(token);

            var result = _service.RefreshToken(oldToken);

            result.UserData.Should().Be(user);
            result.AccessToken.Should().Be(token);
        }
    }
}
