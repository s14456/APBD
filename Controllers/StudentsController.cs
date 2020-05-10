using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{

    
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        public IStudentsDbService _studentDbService;

        public StudentsController(IStudentsDbService studentsDbService)
        {
            _studentDbService = studentsDbService;
        }
        /*
         * HttpGet -> pobierz
         * HttpPost -> dodaj
         * HttpPut -> zaktualizuj
         * HttpPatch -> zalataj
         * HttpDelete -> usun
         */

        [HttpGet]
        public IActionResult GetStudent()
        {
            return Ok(_studentDbService.GetStudent());
        }

        [HttpGet("{IndexNumber}")]

        public IActionResult GetEnrollment(int IndexNumber)
        {
            return Ok(_studentDbService.GetEnrollment(IndexNumber));
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            //add to db
            //generate id
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPost("{id}")]
        public IActionResult GetStudentById([FromRoute]int id)
        {
            if(id == 1)
            {
                //cokolwiek
                return Ok("Kowalski");
            }
            else if(id == 2)
            {
                return Ok("Malewski");
            }
            else
                return NotFound("Nie znaleziono studenta");
        }


        [HttpPut("{id}")]

        public IActionResult UpdateStudent(int id)
        {
            return Ok("Aktualizacja dokonczona.");
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteStudent(int id)
        {

            return Ok("Usuwanie ukonczone.");
        }
    }
}