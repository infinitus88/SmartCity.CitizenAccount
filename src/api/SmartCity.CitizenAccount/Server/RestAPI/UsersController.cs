using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Data.Access.Constants;
using SmartCity.CitizenAccount.Maps;
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
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);

            return Ok();
        }

        [HttpPost("delete-users")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> DeleteUsers([FromBody] RemoveUsersModel model)
        {
            foreach (var userId in model.UserIds)
            {
                await _service.Delete(userId);
            }

            return Ok();
        }

        [HttpGet("get-verif-status")]
        [Authorize]
        public VerificationStatusModel GetVerificationStatus()
        {
            var verificationStatus = _service.GetVerificationStatus(_context.User.Id);

            return _mapper.Map<VerificationStatusModel>(verificationStatus);
        }

        [HttpPost("create-verif-request")]
        [Authorize]
        public async Task<IActionResult> CreateVerificationRequest([FromBody] CreateVerificationRequest model)
        {
            var request = await _service.CreateVerificationRequest(model);

            return new OkObjectResult(_mapper.Map<VerificationRequestModel>(request)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPost("update-verif-request")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> UpdateRequest([FromBody] UpdateVerificationRequestModel model)
        {
            await _service.UpdateVerificationRequest(model);

            return Ok();
        }

        [HttpPost("update-verif-requests")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> UpdateRequests([FromBody] UpdateVerificationRequestsModel model)
        {
            foreach (var id in model.RequestIds)
            {
                await _service.UpdateVerificationRequest(new UpdateVerificationRequestModel { Id = id, Status = model.Status });
            }

            return Ok();
        }

        [HttpGet("verification-status/{id}")]
        [Authorize]
        public VerificationStatusModel GetVerificationStatus(int id)
        {
            var requestStatus = _service.GetVerificationStatus(id);

            return _mapper.Map<VerificationStatusModel>(requestStatus);
        }

        [HttpGet("verification-requests")]
        [Authorize(Roles = Roles.Administrator)]
        public IQueryable<VerificationRequestModel> GetVerificationRequests()
        {
            var requests = _service.GetVerificationRequests();

            return requests.ProjectTo<VerificationRequestModel>(_mapper.ConfigurationProvider);
        }
    }
}
