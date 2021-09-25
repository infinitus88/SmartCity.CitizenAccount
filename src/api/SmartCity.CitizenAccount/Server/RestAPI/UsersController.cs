using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Data.Access.Constants;
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

        [HttpGet]
        [Authorize]
        public IQueryable<UserModel> GetAll()
        {
            var users = _service.Get();

            return users.ProjectTo<UserModel>(_mapper.ConfigurationProvider);
        }

        [HttpGet("{id}")]
        [Authorize]
        public UserModel GetUser(int id)
        {
            var user = _service.Get(id);

            return _mapper.Map<UserModel>(user);
        }

        [HttpPost("update-user")]
        [Authorize]
        public async Task<UserInfoModel> UpdateUserInfo([FromBody] UpdateUserInfoModel model)
        {
            var updateUserModel = _mapper.Map<UpdateUserModel>(_mapper.Map<UserModel>(_context.User));

            var user = await _service.Update(_context.User.Id, updateUserModel);
            

            return new UserInfoModel { UserData = _mapper.Map<UserModel>(user) };
        }

        [HttpPost("update-user/{id}")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<UserModel> UpdateUser(int id, [FromBody] UpdateUserModel model)
        {
            var user = await _service.Update(id, model);

            return _mapper.Map<UserModel>(user);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);

            return Ok();
        }
    }
}
