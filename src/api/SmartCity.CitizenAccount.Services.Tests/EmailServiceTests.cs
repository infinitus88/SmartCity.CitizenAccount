using AutoMapper;
using FluentAssertions;
using Moq;
using SmartCity.CitizenAccount.Api.Common.Exceptions;
using SmartCity.CitizenAccount.Api.Models.Emails;
using SmartCity.CitizenAccount.Data.Access.DAL.Repositories;
using SmartCity.CitizenAccount.Data.Models;
using SmartCity.CitizenAccount.Maps;
using SmartCity.CitizenAccount.Security;
using SmartCity.CitizenAccount.Services.EmailsAppService;
using SmartCity.CitizenAccount.Services.PaymentAppService;
using SmartCity.CitizenAccount.Services.UserAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartCity.CitizenAccount.Services.Tests
{
    public class EmailServiceTests
    {
        private Mock<IGenericRepository> _repository;
        private List<Email> _emailList;
        private List<User> _userList;
        private IEmailsService _service;
        private Random _random;
        private Mock<IUsersService> _userService;
        private Mock<IPaymentService> _paymentService;
        private Mock<ISecurityContext> _context;

        public EmailServiceTests()
        {
            _repository = new Mock<IGenericRepository>();

            _random = new Random();

            _emailList = new List<Email>();
            _repository.Setup(x => x.Query<Email>()).Returns(() => _emailList.AsQueryable());

            _userList = new List<User>();
            _userService = new Mock<IUsersService>();
            _userService.Setup(x => x.Get()).Returns(() => _userList.AsQueryable());

            _paymentService = new Mock<IPaymentService>();

            _context = new Mock<ISecurityContext>(MockBehavior.Strict);

            _service = new EmailService(_repository.Object, _userService.Object, _paymentService.Object, _context.Object);
        }

        [Fact]
        public void GetRelatedShouldReturnOnlyRelatedEmails()
        {
            var user = new User
            {
                Id = _random.Next(),
                Status = Status.Active
            };

            _emailList.Add(new Email { Id = _random.Next(), UserId = user.Id });
            _emailList.Add(new Email { Id = _random.Next(), UserId = _random.Next() });
            _context.SetupGet(x => x.User).Returns(user);

            var result = _service.GetRelated();
            result.Count().Should().Be(1);
        }

        [Fact]
        public async Task CreateShouldSaveNewEmailForSender()
        {
            var sender = new User
            {
                Id = _random.Next(),
                Email = _random.Next().ToString(),
                Status = Status.Active
            };

            var recipient = new User
            {
                Id = _random.Next(),
                Email = _random.Next().ToString(),
                Status = Status.Active
            };

            _userList.Add(recipient);

            var model = new CreateEmailModel
            {
                EmailAddress = recipient.Email,
                Subject = _random.Next().ToString(),
                Message = _random.Next().ToString()
            };

            _context.SetupGet(x => x.User).Returns(sender);

            var result = await _service.Create(model);

            result.EmailAddress.Should().Be(model.EmailAddress);
            result.Subject.Should().Be(model.Subject);
            result.Message.Should().Be(model.Message);
        }

        [Fact]
        public void CreateShouldThrowExceptionIfUserIsNotFoundByEmail()
        {
            _userList.Add(new User { Id = _random.Next(), Email = _random.Next().ToString() });

            var model = new CreateEmailModel
            {
                EmailAddress = _random.Next().ToString(),
                Subject = _random.Next().ToString(),
                Message = _random.Next().ToString()
            };

            Action create = () =>
            {
                var x = _service.Create(model).Result;
            };

            create.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task UpdateFolderShouldUpdateEmailsFolderField()
        {
            var email = new Email
            {
                Id = _random.Next(),
                Folder = _random.Next().ToString()
            };

            _emailList.Add(email);

            var newFolder = _random.Next().ToString();

            var result = await _service.UpdateFolder(email.Id, newFolder);

            result.Folder.Should().Be(newFolder);
        }

        [Fact]
        public void UpdateShouldThrowExceptionIfUserIsNotFoundById()
        {
            var email = new Email { Id = _random.Next() };
            _emailList.Add(email);

            Action updateFolder = () =>
            {
                var x = _service.UpdateFolder(_random.Next(), _random.Next().ToString()).Result;
            };

            updateFolder.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task SetStarredShouldUpdateEmailsIsStarredField()
        {
            var email = new Email
            {
                Id = _random.Next(),
                Folder = _random.Next().ToString(),
                IsStarred = false
            };

            _emailList.Add(email);

            var result = await _service.SetStarred(email.Id, true);

            result.IsStarred.Should().BeTrue();
        }

        [Fact]
        public void SetStarredShouldThrowExceptionIfEmailIsNotFoundById()
        {
            var email = new Email { Id = _random.Next(), IsStarred = false };
            _emailList.Add(email);

            Action setStarred = () =>
            {
                var x = _service.SetStarred(_random.Next(), true).Result;
            };

            setStarred.Should().Throw<NotFoundException>();
        }

        [Fact]
        public async Task MarkUnreadShouldUpdateEmailUnreadField()
        {
            var email = new Email
            {
                Id = _random.Next(),
                Unread = false
            };

            _emailList.Add(email);

            var result = await _service.MarkUnread(email.Id, true);

            result.Unread.Should().BeTrue();
        }

        [Fact]
        public void MarkUnreadShouldThrowIfEmailIsNotFoundById()
        {
            var email = new Email
            {
                Id = _random.Next(),
                Unread = false
            };

            _emailList.Add(email);

            Action markUnread = () =>
            {
                var x = _service.MarkUnread(_random.Next(), true).Result;
            };

            markUnread.Should().Throw<NotFoundException>();
        }

    }
}
