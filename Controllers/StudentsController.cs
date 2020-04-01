using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{

    
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private const string ConString = "Data Source=db-mssql16;Initial Catalog=s14456;Integrated Security=True";
       
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
            var list = new List<Student>();
           
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student";

                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    //IndexNumber, FirstName, LastName, BirthDate, IdEnrollment
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.IdEnrollement = dr.GetInt32(4);
                        //["IdEnrollement"];
                    list.Add(st);
                }
            }

            return Ok(list);
        }

        [HttpGet("{IndexNumber}")]

        public IActionResult GetEnrollment(int IndexNumber)
        {
            int id = IndexNumber;
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select Semester from student join Enrollment on Student.IdEnrollment=Enrollment.IdEnrollment where IndexNumber=@IndexNumber";
                com.Parameters.AddWithValue("IndexNumber", id);
                con.Open();
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    int semester = dr.GetInt32(0);
                   return Ok("Semester studenta: " +semester);

                }
                return NotFound();

            }

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