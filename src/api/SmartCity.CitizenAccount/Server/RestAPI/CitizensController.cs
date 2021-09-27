﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCity.CitizenAccount.Api.Models.Citizens;
using SmartCity.CitizenAccount.Data.Access.Constants;
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
        private readonly ICitizensService _service;
        private readonly IMapper _mapper;

        public CitizensController(ICitizensService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IQueryable<CitizenModel> GetAll()
        {
            var citizens = _service.Get();

            return citizens.ProjectTo<CitizenModel>(_mapper.ConfigurationProvider);
        }

        [HttpGet("get/{id}")]
        public ActionResult<CitizenModel> GetCitizenById(string id)
        {
            var citizen = _service.Get(id);

            return Ok(citizen);
        }

        [HttpPost("register")]
        public async Task<CitizenModel> RegisterCitizen([FromBody] CreateCitizenModel input)
        {
            var citizen = await _service.Create(input);

            return _mapper.Map<CitizenModel>(citizen);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.Delete(id);

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> DeleteUsers([FromBody] RemoveCitizensModel model)
        {
            foreach (var citizenId in model.CitizenIds)
            {
                await _service.Delete(citizenId);
            }

            return Ok();
        }
    }
}
