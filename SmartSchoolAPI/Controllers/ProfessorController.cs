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
    public class ProfessorController : ControllerBase
    {
        public ProfessorController()
        {
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok();
        }


    }
}