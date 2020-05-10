using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class StudentsDbServices : IStudentsDbService
    {

        private const string ConString = "Data Source=db-mssql16;Initial Catalog=s14456;Integrated Security=True";

        public Student Entrollment(Student newStudent)
        {

            var st = new Student();

            using (SqlConnection con = new SqlConnection())
            using (SqlCommand com = new SqlCommand())
            {
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    com.Connection = con;
                    com.Transaction = tran;
                    com.CommandText = "select * from studies where name = '" + newStudent.Studies + "'";
                    SqlDataReader dr = com.ExecuteReader();
                    con.Open();
                    if (!dr.Read())
                    {
                        //BadRequest
                        
                        return null;
                    }

                    com.CommandText = "select * from Enrollment where semester = 1 and StartDate = (select MAX(StartDate) as date from Enrollment); ";
                    dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        com.CommandText = "insert into Enrollment values (((select MAX(IdEnrollment) from enrollment) + 1), 1, 4, getdate());";
                    }
                    var list = new List<string>();
                    com.CommandText = "select IndexNumber from student";
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        list.Add(dr["IndexNumber"].ToString());
                        if (list.Contains(newStudent.IndexNumber))
                        {
                            //BadRequest
                            
                            return null;
                        }
                    }


                    st.IndexNumber = newStudent.IndexNumber;
                    st.FirstName = newStudent.FirstName;
                    st.LastName = newStudent.LastName;
                    st.BirthDate = newStudent.BirthDate;
                    st.Studies = newStudent.Studies;

                    com.CommandText = "insert into student values('" + st.IndexNumber + "', '" + st.FirstName + "', '" + st.LastName + "', '" + st.BirthDate + "', (select idenrollment from enrollment, studies where semester = 1 and studies.name = '" + st.Studies + "'))";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine(ex2);
                    }

                }
            }

             return st;
        }

        public int GetEnrollment(int IndexNumber)
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
                    return semester ;
                }
                return 0;

            }
        }

        public IEnumerable<Student> GetStudent()
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
            return list;
        }

        public static List<string> GetAllIndex()
        {
            List<string> indexStrings = new List<string>();
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select IndexNumber from student";
                con.Open();
                SqlDataReader dr = com.ExecuteReader();

                while (dr.Read())
                {
                    indexStrings.Add(dr.ToString());
                }
            } 
                return indexStrings;
        }

        public void PromoteStudent()
        {
            using (SqlConnection con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "promote_student";
                com.CommandType = CommandType.StoredProcedure;
                // com.Parameters.AddWithValue();

                var dr = com.ExecuteReader();

                if (dr.Read())
                {

                }
            }
        }
    }
}
