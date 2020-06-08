using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IStudentsDbService
    {
        public int GetEnrollment(int IndexNumber);

        public IEnumerable<Student> GetStudent();

        public Student Entrollment(Student student);

        public void PromoteStudent();

        public void ModifyStudent(Student student);
        public void GetStudents();
        public void DeleteStudent(string id);
    }

}
