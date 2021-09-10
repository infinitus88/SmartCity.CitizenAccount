using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartCity.CitizenAccount.Api.Models.Emails;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Services.UserAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Services.EmailsAppService
{
    public interface IEmailsService
    {
        IQueryable<Email> GetRelated(string email);
        Task<Email> Create(CreateEmailModel model);
        Task<Email> UpdateFolder(string folderName);
    }

    public class EmailService : IEmailsService
    {
        private readonly IGenericRepository _repository;
        private readonly IUsersService _userService;
        private readonly IMapper _mapper;

        public EmailService(IGenericRepository repository, IUsersService userService, IMapper mapper)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
        }

        public IQueryable<Email> GetRelated(string email)
        {
            var mails = _repository.Query<Email>().Where(e => e.Sender == email || e.To == email);

            var query = mails.ProjectTo<EmailModel>(_mapper.ConfigurationProvider);

            return mails;
        }

        public Task<Email> Create(CreateEmailModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Email> UpdateFolder(string folderName)
        {
            throw new NotImplementedException();
        }
    }
}
