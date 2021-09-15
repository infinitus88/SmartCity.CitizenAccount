﻿using SmartCity.CitizenAccount.Api.Models.Emails;
using SmartCity.CitizenAccount.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Services.EmailsAppService
{
    public interface IEmailsService
    {
        IQueryable<Email> GetAll();
        IQueryable<Email> GetRelated();
        Task<Email> Create(CreateEmailModel model);
        Task<Email> UpdateFolder(int id, string folderName);
    }
}
