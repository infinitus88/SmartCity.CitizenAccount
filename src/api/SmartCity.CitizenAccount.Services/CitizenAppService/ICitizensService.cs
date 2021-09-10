using SmartCity.CitizenAccount.Api.Models.Citizens;
using SmartCity.CitizenAccount.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Services.CitizenAppService
{
    public interface ICitizensService
    {
        IQueryable<Citizen> Get();
        Citizen Get(string id);
        Task<Citizen> Create(CreateCitizenModel input);
    }
}
