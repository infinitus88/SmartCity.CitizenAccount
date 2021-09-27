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

        public async Task<VerificationRequest> CreateVerificationRequest(CreateVerificationRequest model)
        {
            var user = GetQuery().Where(u => u.Id == model.UserId).FirstOrDefault();
            var citizen = _repository.Query<Citizen>().Where(c => c.Id == model.CitizenId).FirstOrDefault();

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            if (citizen == null)
            {
                throw new NotFoundException("Citizen is not found");
            }
            var request = _mapper.Map<VerificationRequest>(model);
            _repository.Add(request);

            await _repository.SaveAsync();

            return request;
        }

        public async Task<VerificationRequest> UpdateVerificationRequest(UpdateVerificationRequestModel model)
        {
            var request = _repository.Query<VerificationRequest>().Where(c => c.Id == model.Id).FirstOrDefault();
            var user = GetQuery().Where(u => u.Id == request.UserId).FirstOrDefault();

            if (request == null)
            {
                throw new NotFoundException("Verification Request is not found");
            }

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            var updateModel = _mapper.Map<VerificationRequest>(model);

            request.Status = updateModel.Status;
            
            if (updateModel.Status == VerificationStatus.Accepted)
            {
                user.CitizenId = request.CitizenId;
                user.IsVerified = true;
            }

            await _repository.SaveAsync();

            return request;
        }

        public IQueryable<VerificationRequest> GetVerificationRequests()
        {
            var requests = _repository.Query<VerificationRequest>().Where(r => r.Status == VerificationStatus.Pending);

            return requests;
        }

        public VerificationStatus GetVerificationStatus(int userId)
        {
            var requests = _repository.Query<VerificationRequest>().Where(r => r.UserId == userId);
            var latestRequest = requests.FirstOrDefault(r => r.CreationTime == requests.Max(x => x.CreationTime));

            if (latestRequest == null)
            {
                return VerificationStatus.NotSent;
            }

            return latestRequest.Status;
        }
    }
}
