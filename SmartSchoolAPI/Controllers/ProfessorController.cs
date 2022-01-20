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
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok(_context.Professores);
        }

        [HttpGet("ById")]
        public IActionResult GetById (int id)
        { 
            var professor = _context.Professores.Where(a => a.Id == id);
            if(professor == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(professor);
        }

        [HttpGet("ByName")]
        public IActionResult GetByName(string nome) 
        {
            var professor = _context.Professores.Where(a => a.Nome == nome);
            if(professor == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor) 
        {
            _context.Professores.Add(professor);
            _context.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor) 
        {
            if(_context.Professores.Where(a => a.Id == id) == null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Professores.Update(professor);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor) 
        {
            if(_context.Professores.Where(a => a.Id == id) == null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Professores.Update(professor);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if(professor == null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Professores.Remove(professor);
            _context.SaveChanges();
            return Ok();


        }



    }
}