using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Access.Helpers;
using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Services.UserAppService
{
    public class UsersService : IUsersService
    {
        private readonly IGenericRepository _repository;
        private readonly IMapper _mapper;

        public UsersService(IGenericRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IQueryable<User> Get()
        {
            var query = GetQuery();

            return query;
        }

        private IQueryable<User> GetQuery()
        {
            return _repository.Query<User>()
                .Where(x => !x.IsDeleted);
        }

        public User Get(int id)
        {
            var user = GetQuery().FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            return user;
        }

        public async Task<User> Create(CreateUserModel model)
        {
            var email = model.Email.Trim();

            if (GetQuery().Any(u => u.Email == email))
            {
                throw new BadRequestException("The email is already in use");
            }

            var user = _mapper.Map<User>(model);
            user.Password = user.Password.WithBCrypt();

            _repository.Add(user);
            await _repository.SaveAsync();

            return user;
        }

        public async Task<User> Update(int id, UpdateUserModel model)
        {
            var user = GetQuery().FirstOrDefault(x => x.Id == id);
            var updateModel = _mapper.Map<User>(model);
            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            //if (GetQuery().Any(x => x.Email == model.Email))
            //{
            //    throw new BadRequestException("The email is already in use");
            //}

            user.About = updateModel.About;
            user.CitizenId = updateModel.CitizenId;
            user.DisplayName = updateModel.DisplayName;
            user.IsVerified = updateModel.IsVerified;
            user.Role = updateModel.Role;
            user.Status = updateModel.Status;
            user.PhotoUrl = updateModel.PhotoUrl;


            await _repository.SaveAsync();

            return user;
            
        }

        public async Task Delete(int id)
        {
            var user = GetQuery().FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            if (user.IsDeleted) return;

            user.IsDeleted = true;
            await _repository.SaveAsync();
        }

        public async Task ChangePassword(int id, ChangeUserPasswordModel model)
        {
            var user = Get(id);
            user.Password = model.Password.Trim().WithBCrypt();
            await _repository.SaveAsync();
        }
    }
}
