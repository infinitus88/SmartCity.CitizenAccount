using SmartCity.CitizenAccount.Api.Models.Citizens;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCity.CitizenAccount.Services.CitizenAppService
{
    public interface ICitizenService
    {
        IQueryable<CitizenModel> GetAllCitizens();
        CitizenModel GetCitizenById(string id);
        CitizenModel RegisterCitizen(RegisterCitizenModel input);
    }
}
