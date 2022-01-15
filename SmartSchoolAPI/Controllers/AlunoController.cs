using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using SmartSchoolAPI.Models;

namespace SmartSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public AlunoController(){}

        [HttpGet]
        public IActionResult Get() 
        {

            return Ok(" Deveriam haver alunos aqui");
        }

    }
}