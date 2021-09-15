using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCity.CitizenAccount.Api.Models.Emails;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Security;
using SmartCity.CitizenAccount.Services.EmailsAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Server.RestAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailsService _service;
        private readonly IMapper _mapper;

        public EmailController(IEmailsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Route("mails")]
        [Authorize]
        public IQueryable<EmailModel> GetMails([FromQuery]string filter)
        {
            var mails = _service.GetRelated().Where(m => m.Folder == filter);

            return mails.ProjectTo<EmailModel>(_mapper.ConfigurationProvider);
        }

        [Route("all-mails")]
        public IQueryable<Email> GetAllMails()
        {
            var mails = _service.GetAll();

            return mails;
        }

        [Route("send")]
        [Authorize]
        public async Task<EmailModel> SendMail([FromBody] CreateEmailModel model)
        {
            var mail = await _service.Create(model);

            return _mapper.Map<EmailModel>(mail);
        }
    }
}
