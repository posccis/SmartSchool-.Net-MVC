using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {


        public readonly IRepository _repo;

        public AlunoController(IRepository repo)
        {
  
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var result = _repo.GetAlunos(true);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id) 
        {
            var aluno = _repo.GetAlunoById(Id);

            if (aluno == null) return StatusCode(StatusCodes.Status404NotFound);


            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            if(_repo.SaveChanges()) return Ok(aluno);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno) 
        {
            var alu = _repo.GetAlunoById(id);
            if(alu == null) return StatusCode(StatusCodes.Status404NotFound);
            _repo.Update(aluno);
            if(_repo.SaveChanges()) return Ok(aluno);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno) 
        {
            if(_repo.GetAlunoById(id) == null) return StatusCode(StatusCodes.Status404NotFound);

            _repo.Update(aluno);
            if(_repo.SaveChanges()) return Ok(aluno);

            return StatusCode(StatusCodes.Status400BadRequest);
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var aluno = _repo.GetAlunoById(id);

            if (aluno == null) return StatusCode(StatusCodes.Status404NotFound);
            _repo.Delete(aluno);
            if(_repo.SaveChanges()) return StatusCode(StatusCodes.Status200OK);

            return StatusCode(StatusCodes.Status400BadRequest); 

        }

    }
}