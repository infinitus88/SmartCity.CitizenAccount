using SmartCity.CitizenAccount.Api.Models.Emails;
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
        EmailsMetaModel GetMeta();
        Task<Email> CreateFromService(int serviceId, CreateEmailModel model);
        Task<Email> Create(CreateEmailModel model);
        Task<Email> UpdateFolder(int id, string folderName);
        Task<Email> SetStarred(int id, bool value);
        Task<Email> MarkUnread(int id, bool unreadFlag);
    }
}