using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Services.UserAppService
{
    public interface IUsersService
    {
        IQueryable<User> Get();
        User Get(int id);
        Task<User> Create(CreateUserModel model);
        Task<User> Update(int id, UpdateUserModel model);
        Task Delete(int id);
        Task ChangePassword(int id, ChangeUserPasswordModel model);
    }
}
