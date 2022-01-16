using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id = 1,
                Nome = "Marcos",
                Sobrenome= "Gomes",
                Telefone = "12345678"
            },
            new Aluno(){
                Id = 2,
                Nome = "Luis",
                Sobrenome= "Hnerique",
                Telefone = "12345678"
            },
            new Aluno(){
                Id = 3,
                Nome = "Silva",
                Sobrenome= "Gadelha",
                Telefone = "12345678"
            }
        };
        public AlunoController(){}

        [HttpGet]
        public IActionResult Get() 
        {

            return Ok(Alunos);
        }

        [HttpGet("ById")]
        public IActionResult GetById(int Id) 
        {
            var aluno = Alunos.Find(a => Id == a.Id);

            if (aluno == null) return StatusCode(StatusCodes.Status404NotFound);


            return Ok(aluno);
        }

        [HttpGet("ByName")]
        public IActionResult GetByName(string nome) 
        { 
            var aluno = Alunos.Find(a => nome == a.Nome);

            if(aluno == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(aluno);
        }

    }
}