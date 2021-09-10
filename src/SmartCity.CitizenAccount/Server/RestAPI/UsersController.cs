using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Security;
using SmartCity.CitizenAccount.Services.UserAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Server.RestAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        private readonly ISecurityContext _context;
        private readonly IMapper _mapper;

        public UsersController(IUsersService service, IMapper mapper, ISecurityContext context)
        {
            _service = service;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("get")]
        [Authorize]
        public IQueryable<UserModel> GetAll()
        {
            var users = _service.Get();

            return users.ProjectTo<UserModel>(_mapper.ConfigurationProvider);
        }

        [HttpPost("updateUserInfo")]
        [Authorize]
        public async Task<UserInfoModel> UpdateUser([FromBody] UpdateUserInfoModel model)
        {
            var updateUserModel = new UpdateUserModel
            {
                DisplayName = model.DisplayName,
                PhotoUrl = model.PhotoURL,
                Email = _context.User.Email,
                About = _context.User.About
            };

            var user = await _service.Update(_context.User.Id, updateUserModel);

            return new UserInfoModel { UserData = _mapper.Map<UserModel>(user) };
        }

    }
}
