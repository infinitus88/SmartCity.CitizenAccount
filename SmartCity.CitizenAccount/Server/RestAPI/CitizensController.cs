using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCity.CitizenAccount.Api.Models.Citizens;
using SmartCity.CitizenAccount.Services.CitizenAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Server.RestAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitizensController : ControllerBase
    {
        private readonly ICitizenService _service;

        public CitizensController(ICitizenService service)
        {
            _service = service;
        }

        [HttpGet]
        public IQueryable<CitizenModel> GetAll()
        {
            var citizens = _service.GetAllCitizens();

            return citizens;
        }

        [HttpGet("get/{id}")]
        public ActionResult<CitizenModel> GetCitizenById(string id)
        {
            var citizen = _service.GetCitizenById(id);

            return Ok(citizen);
        }

        [HttpPost("register")]
        public ActionResult<CitizenModel> RegisterCitizen([FromBody] RegisterCitizenModel input)
        {
            var citizen = _service.RegisterCitizen(input);

            return new ObjectResult(citizen) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
