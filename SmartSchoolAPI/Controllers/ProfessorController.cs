using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolAPI.Data;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;

        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok(_repo.GetProfessor());
        }

        [HttpGet("{id}")]
        public IActionResult GetById (int id)
        { 
            var professor = _repo.GetAlunoById(id);
            if(professor == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor) 
        {
            _repo.Add(professor);
            if(_repo.SaveChanges()) return Ok(professor);

            return StatusCode(StatusCodes.Status400BadRequest);;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor) 
        {
            if(_repo.GetAlunoById(id) == null) return StatusCode(StatusCodes.Status404NotFound);

            _repo.Update(professor);
            if(_repo.SaveChanges()) return Ok();

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor) 
        {
            if(_repo.GetAlunoById(id) == null) return StatusCode(StatusCodes.Status404NotFound);

            _repo.Update(professor);
            if(_repo.SaveChanges()) return Ok();

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var professor = _repo.GetProfessorById(id);
            if(professor == null) return StatusCode(StatusCodes.Status404NotFound);

            _repo.Delete(professor);
            if(_repo.SaveChanges()) return Ok();
            
            return StatusCode(StatusCodes.Status400BadRequest);


        }



    }
}