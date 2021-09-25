using SmartCity.CitizenAccount.Api.Models.Auth;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Security;
using SmartCity.CitizenAccount.Security.Auth;
using SmartCity.CitizenAccount.Services.UserAppService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Data.Access.Common;
using SmartCity.CitizenAccount.Data.Access.Helpers;
using SmartCity.CitizenAccount.Data.Models.Constants;

namespace SmartCity.CitizenAccount.Services.AuthAppService
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository _repository;
        private readonly ITokenBuilder _tokenBuilder;
        private readonly IUsersService _usersService;
        private readonly ISecurityContext _context;
        private Random _random;

        public AuthService(IGenericRepository repository, ITokenBuilder tokenBuilder, IUsersService usersService, ISecurityContext context)
        {
            _repository = repository;
            _tokenBuilder = tokenBuilder;
            _usersService = usersService;
            _context = context;
            _random = new Random();
        }

        public UserWithToken Authenticate(string email, string password)
        {
            var user = (from u in _repository.Query<User>().Where(u => u.Status == Status.Active)
                            where u.Email == email && !u.IsDeleted
                        select u).FirstOrDefault();

            if (user == null)
            {
                throw new BadRequestException("email/password aren't right");
            }

            if (string.IsNullOrWhiteSpace(password) || !user.Password.VerifyWithBCrypt(password))
            {
                throw new BadRequestException("email/password aren't right");
            }

            var expiresIn = DateTime.Now + TokenAuthOption.ExpiresSpan;
            var accessToken = _tokenBuilder.Build(user.Email, user.Role, expiresIn);

            return new UserWithToken
            {
                AccessToken = accessToken,
                UserData = user
            };
        }

        public async Task<User> Register(RegisterModel model)
        {
            var requestModel = new CreateUserModel
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Password = model.Password
            };

            var user = await _usersService.Create(requestModel);
            return user;
        }

        public async Task ChangePassword(ChangeUserPasswordModel model)
        {
            await _usersService.ChangePassword(_context.User.Id, model);
        }

        public string RefreshToken()
        {
            var expiresIn = DateTime.Now + TokenAuthOption.ExpiresSpan;
            var accessToken = _tokenBuilder.Build(_context.User.Email, _context.User.Role, expiresIn);

            return accessToken;
        }
    }
}
