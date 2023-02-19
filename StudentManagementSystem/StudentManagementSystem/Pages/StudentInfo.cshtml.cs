using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentManagementSystem.Pages
{

    public class StudentModel
    {
        public int studentId;
        public string studentName;
        public string city;
        public string course;
        public string instructor;
    }
    public class StudentInfoModel : PageModel
    {
        public void OnGet()
        {
            string connectionStr = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=studentManagement;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionStr);

            string query = "SELECT studentId, student.name AS studentName, city, course.name AS courseName, lecturerName FROM student JOIN course ON student.courseId=course.courseId";

            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            List<StudentModel> data = new List<StudentModel>();

            while(reader.Read())
            {
                StudentModel row = new StudentModel();
                row.studentId = (int)reader["studentId"];
                row.studentName = (string)reader["studentName"];
                row.city = (string)reader["city"];
                row.course = (string)reader["courseName"];
                row.instructor = (string)reader["lecturerName"];
                data.Add(row);
            }

            ViewData["data"] = data;
            conn.Close();
        }
    }
}
