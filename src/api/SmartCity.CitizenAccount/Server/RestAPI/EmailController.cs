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

        [HttpGet("mails")]
        [Authorize]
        public IQueryable<EmailModel> GetMails([FromQuery]string filter)
        {
            var mails = _service.GetRelated();

            if (filter == "starred")
            {
                mails = mails.Where(m => m.IsStarred);
            } 
            else
            {
                mails = mails.Where(m => m.Folder == filter);
            }

            return mails.ProjectTo<EmailModel>(_mapper.ConfigurationProvider);
        }

        [HttpGet("meta")]
        [Authorize]
        public EmailsMetaModel GetEmailsMeta()
        {
            var emailsMeta = _service.GetMeta();

            return emailsMeta;
        }

        [HttpPost("send-mail-service/{id}")]
        public async Task<EmailModel> SendMailService(int id, [FromBody] CreateEmailModel model)
        {
            var mail = await _service.CreateFromService(id, model);

            return _mapper.Map<EmailModel>(mail);
        }

        [HttpGet("all-mails")]
        public IQueryable<Email> GetAllMails()
        {
            var mails = _service.GetAll();

            return mails;
        }

        [HttpPost("send-mail")]
        [Authorize]
        public async Task<EmailModel> SendMail([FromBody] CreateEmailModel model)
        {
            var mail = await _service.Create(model);

            return _mapper.Map<EmailModel>(mail);
        }

        [HttpPost("move-mails")]
        [Authorize]
        public async Task<EmailsMetaModel> MoveMails([FromBody] MoveMailsModel model)
        {
            foreach(var id in model.EmailIds)
            {
                await _service.UpdateFolder(id, model.Folder);
            }

            return _service.GetMeta();
        }

        [HttpPost("set-starred")]
        [Authorize]
        public async Task<IActionResult> SetStarred([FromBody] SetStarredModel model)
        {
            await _service.SetStarred(model.MailId, model.Value);

            return Ok();
        }

        [HttpPost("mark-unread")]
        [Authorize]
        public async Task<EmailsMetaModel> MarkUnread([FromBody] MarkUnreadModel model)
        {
            var emailsMeta = new EmailsMetaModel();
            foreach (var id in model.EmailIds)
            {
                var mail = await _service.MarkUnread(id, model.UnreadFlag);
            }
            

            return _service.GetMeta();
        }
    }
}
