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
        private readonly SmartContext _context;

        public readonly IRepository _repo;

        public AlunoController(SmartContext context,
                            IRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() 
        {

            return Ok(_context.Alunos);
        }

        [HttpGet("ById")]
        public IActionResult GetById(int Id) 
        {
            var aluno = _context.Alunos.Where(a => a.Id == Id);

            if (aluno == null) return StatusCode(StatusCodes.Status404NotFound);


            return Ok(aluno);
        }

        [HttpGet("ByName")]
        public IActionResult GetByName(string nome) 
        { 
            var aluno = _context.Alunos.Where(a => nome == a.Nome);

            if(aluno == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno) 
        {
            var alu = _context.Alunos.Where(a => a.Id == id);
            if(alu == null) return StatusCode(StatusCodes.Status404NotFound);
            _context.Alunos.Update(aluno);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno) 
        {
            if(_context.Alunos.Where(a => a.Id == id) == null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Alunos.Update(aluno);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);

            if (aluno == null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK); 

        }

    }
}