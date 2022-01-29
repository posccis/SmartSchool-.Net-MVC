using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolAPI.Data;
using SmartSchoolAPI.Dtos;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _map;

        public ProfessorController(IRepository repo, IMapper map)
        {
            _repo = repo;
            _map = map;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var professores = _repo.GetProfessor();
            return Ok(_map.Map<IEnumerable<ProfessorDto>>(professores));
        }

        [HttpGet("{id}")]
        public IActionResult GetById (int id)
        { 
            var professor = _repo.GetProfessorById(id);

            if(professor == null) return StatusCode(StatusCodes.Status404NotFound);

            var retorno = _map.Map<ProfessorDto>(professor);

            return Ok(retorno);
        }

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDtos model) 
        {
            var professor = _map.Map<Professor>(model);

            _repo.Add(professor);

            if(_repo.SaveChanges()) return StatusCode(StatusCodes.Status201Created, "Professor criado");

            return StatusCode(StatusCodes.Status400BadRequest);;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDtos model) 
        {
            if(_repo.GetProfessorById(id) == null) return StatusCode(StatusCodes.Status404NotFound);

            var professor = _map.Map<Professor>(model);

            _repo.Update(professor);

            if(_repo.SaveChanges()) return Ok("Updated.");

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDtos model) 
        {
            if(_repo.GetProfessorById(id) == null) return StatusCode(StatusCodes.Status404NotFound);

            var professor = _map.Map<Professor>(model);

            _repo.Update(professor);

            if(_repo.SaveChanges()) return Ok("Updated.");

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var professor = _repo.GetProfessorById(id);
            if(professor == null) return StatusCode(StatusCodes.Status404NotFound);

            _repo.Delete(professor);
            if(_repo.SaveChanges()) return Ok("Removed.");
            
            return StatusCode(StatusCodes.Status400BadRequest);


        }



    }
}