using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{

    
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        public StudentsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
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

        //[HttpGet("Students")]
        //public IActionResult GetStudents();
        //{
        //    return Ok(_studentDbService.GetStudents());
        //}

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
            _studentDbService.DeleteStudent(id);
            return Ok("Usuwanie ukonczone.");
        }


        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "jan123"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "student")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            }); ;
        }

        [HttpPost("refresh-token/{token}")]
        public IActionResult RefreshToken(string refreshToken)
        {
            //if(refreshToken.Equals(token))
            return Ok();
        }
    }
}