using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SmartCity.CitizenAccount.Data.Access.Constants;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Security
{
    public class SecurityContext : ISecurityContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenericRepository _repository;

        public SecurityContext(IHttpContextAccessor contextAccessor, IGenericRepository repository)
        {
            _contextAccessor = contextAccessor;
            _repository = repository;
        }

        private User _user;

        public User User
        {
            get
            {
                if (_user != null) return _user;

                if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    throw new UnauthorizedAccessException();
                }

                var email = _contextAccessor.HttpContext.User.Identity.Name;
                _user = _repository.Query<User>()
                    .Where(x => x.Email == email)
                    .FirstOrDefault();

                if (_user == null)
                {
                    throw new UnauthorizedAccessException("User is not found");
                }

                return _user;
            }
        }

        public bool IsAdministrator
        {
            get { return User.Role == Roles.Administrator; }
        }
    }
}
