using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolAPI.Data;
using SmartSchoolAPI.Dtos;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {


        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
  
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            var alunos = _repo.GetAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id) 
        {
            var aluno = _repo.GetAlunoById(Id);

            if (aluno == null) return StatusCode(StatusCodes.Status404NotFound);

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Add(aluno);
            if(_repo.SaveChanges()) return StatusCode(StatusCodes.Status201Created);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model) 
        {
            var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return StatusCode(StatusCodes.Status404NotFound);

            _mapper.Map(model, aluno);

            _repo.Update(aluno);
            if(_repo.SaveChanges()) return Ok(aluno);

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model) 
        {
            var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return StatusCode(StatusCodes.Status404NotFound);

            _mapper.Map(model, aluno);

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