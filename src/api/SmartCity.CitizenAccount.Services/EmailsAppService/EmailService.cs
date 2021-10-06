﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Emails;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Security;
using SmartCity.CitizenAccount.Services.PaymentAppService;
using SmartCity.CitizenAccount.Services.UserAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Services.EmailsAppService
{
    public class EmailService : IEmailsService
    {
        private readonly IGenericRepository _repository;
        private readonly IUsersService _userService;
        private readonly ISecurityContext _context;
        private readonly IPaymentService _paymentService;

        public EmailService(IGenericRepository repository, IUsersService userService, IPaymentService paymentService, ISecurityContext context)
        {
            _repository = repository;
            _userService = userService;
            _context = context;
            _paymentService = paymentService;
        }

        private IQueryable<Email> GetQuery()
        {
            var query = _repository.Query<Email>();

            return query;
        }

        public IQueryable<Email> GetRelated()
        {
            var mails = _repository.Query<Email>().Where(m => m.UserId == _context.User.Id);

            return mails;
        }

        public async Task<Email> CreateFromService(int serviceId, CreateEmailModel model)
        {
            var user = _userService.Get().Where(u => u.Email == model.EmailAddress).FirstOrDefault();
            var service = _paymentService.GetServiceById(serviceId);

            if (user == null)
            {
                throw new NotFoundException("User with this email address is not found");
            }

            if (service == null)
            {
                throw new NotFoundException("Service is not found");
            }

            var email = new Email
            {
                UserId = user.Id,
                DisplayName = service.Name,
                EmailAddress = user.Email,
                Folder = "inbox",
                Subject = model.Subject,
                Message = model.Message,
                Unread = true,
                PhotoUrl = service.ImageUrl
            };
            _repository.Add(email);

            await _repository.SaveAsync();

            return email;
        }

        public async Task<Email> Create(CreateEmailModel model)
        {
            var recievingUser = _userService.Get().Where(u => u.Email == model.EmailAddress).FirstOrDefault();

            if (recievingUser == null)
            {
                throw new NotFoundException("User with this email adress is not found");
            }

            var sendersMail = new Email
            {
                UserId = _context.User.Id,
                DisplayName = recievingUser.DisplayName,
                PhotoUrl = recievingUser.PhotoUrl,
                EmailAddress = model.EmailAddress,
                Subject = model.Subject,
                Message = model.Message,
                Folder = "sent",
                Unread = false
            };
            

            var recieversgMail = new Email
            {
                UserId = recievingUser.Id,
                DisplayName = _context.User.DisplayName,
                PhotoUrl = _context.User.PhotoUrl,
                EmailAddress = _context.User.Email,
                Subject = model.Subject,
                Message = model.Message,
                Folder = "inbox",
                Unread = true
            };

            _repository.Add(sendersMail);
            _repository.Add(recieversgMail);

            await _repository.SaveAsync();

            return sendersMail;
        }

        public async Task<Email> UpdateFolder(int id, string folderName)
        {
            var mail = GetQuery().Where(m => m.Id == id).FirstOrDefault();

            if (mail == null)
            {
                throw new NotFoundException("Email is not found");
            }

            if (mail.Folder == folderName) return mail;

            mail.Folder = folderName;
            await _repository.SaveAsync();

            return mail;
        }

        public async Task<Email> SetStarred(int id, bool value)
        {
            var mail = GetQuery().Where(m => m.Id == id).FirstOrDefault();

            if (mail == null)
            {
                throw new NotFoundException("Email is not found");
            }

            if (mail.IsStarred == value) return mail;

            mail.IsStarred = value;
            await _repository.SaveAsync();

            return mail;
        }

        public async Task<Email> MarkUnread(int id, bool unreadFlag)
        {
            var mail = GetQuery().Where(m => m.Id == id).FirstOrDefault();

            if (mail == null)
            {
                throw new NotFoundException("Email is not found");
            }

            mail.Unread = unreadFlag;

            await _repository.SaveAsync();

            return mail;
        }

        public IQueryable<Email> GetAll()
        {
            var mails = _repository.Query<Email>();

            return mails;
        }

        public EmailsMetaModel GetMeta()
        {
            var mails = GetRelated().Where(m => m.Unread);
            var emailsMeta = new EmailsMetaModel();

            emailsMeta.Folder.Add("inbox", mails.Where(m => m.Folder == "inbox").Count());
            emailsMeta.Folder.Add("trash", mails.Where(m => m.Folder == "trash").Count());

            return emailsMeta;
        }
    }
}
