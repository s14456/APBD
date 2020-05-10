using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        public IStudentsDbService _studentDbService;
        private const string ConString = "Data Source=db-mssql16;Initial Catalog=s14456;Integrated Security=True";

        public EnrollmentController(IStudentsDbService studentsDbService)
        {
            _studentDbService = studentsDbService; 
        }
        
        [HttpPost]
        public IActionResult EnrollStudent([FromBody]Student newStudent)
        {

            return Ok(_studentDbService.Entrollment(newStudent));

        }
        [HttpPost("/promotions")]
        public IActionResult PromoteStudent()
        {
            return Ok();
        }
    }
}