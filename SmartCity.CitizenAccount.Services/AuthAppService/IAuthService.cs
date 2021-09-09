using SmartCity.CitizenAccount.Api.Models.Auth;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Data.Models;
using System.Threading.Tasks;
using SmartCity.CitizenAccount.Data.Access.Common;

namespace SmartCity.CitizenAccount.Services.AuthAppService
{
    public interface IAuthService
    {
        UserWithToken Autheticate(string email, string password);
        Task<User> Register(RegisterModel model);
        Task ChangePassword(ChangeUserPasswordModel model);
    }
}
