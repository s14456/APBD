using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        /*
         * HttpGet -> pobierz
         * HttpPost -> dodaj
         * HttpPut -> zaktualizuj
         * HttpPatch -> zalataj
         * HttpDelete -> usun
         */

        [HttpGet]
        public string GetStudents([FromQuery]string orderBy, [FromQuery] string abc)
        {
            return $"Kowalski, Malewski, Andrzejewski sortowanie={orderBy}";
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            //add to db
            //generate id
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById([FromRoute]int id)
        {
            if(id == 1)
            {
                return Ok("Kowalski");
            }
            else if(id == 2)
            {
                return Ok("Malewski");
            }
            else
                return NotFound("Nie znaleziono studenta");
        }
    }
}