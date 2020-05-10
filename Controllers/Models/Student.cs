using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Student
    {
        //IndexNumber, FirstName, LastName, BirthDate, IdEnrollment
        [Required]
        public string IndexNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string BirthDate { get; set; }
        public int IdEnrollement { get; set; }
        public int Semester { get; set; }
        [Required]
        public string Studies { get; set; }
    }
}
