using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCity.CitizenAccount.Api.Models.Auth;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Services.AuthAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Server.RestAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthController(IAuthService service, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _service = service;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        [HttpPost("login")]
        public UserWithTokenModel Authenticate([FromBody] LoginModel model)
        {
            var result = _service.Authenticate(model.Email, model.Password);
            var resultModel = _mapper.Map<UserWithTokenModel>(result);

            return resultModel;
        }

        [HttpPost("register")]
        public async Task<UserWithTokenModel> Register([FromBody] RegisterModel model)
        {
            var newUser = await _service.Register(model);
            var result = _service.Authenticate(newUser.Email, model.Password);
            return _mapper.Map<UserWithTokenModel>(result);
        }

        [HttpPost("password")]
        [Authorize]
        public async Task ChangePassword([FromBody]ChangeUserPasswordModel model)
        {
            await _service.ChangePassword(model);
        }

        [HttpGet("refresh-token")]
        [Authorize]
        public ActionResult<RefreshTokenModel> RefreshToken()
        {
            string accessToken = _service.RefreshToken();

            return Ok(new RefreshTokenModel { AccessToken = accessToken });
        }
    }
}
