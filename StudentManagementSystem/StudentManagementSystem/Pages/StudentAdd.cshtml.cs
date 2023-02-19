using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;

namespace StudentManagementSystem.Pages
{

    public class MyDataModel
    {
        public int id { get; set; }
        public string courseName { get; set; }
        public string lecturerName { get; set; }
    }

    public class StudentAddModel : PageModel
    {
        public void OnGet()
        {
            string connectionStr = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=studentManagement;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionStr);

            string query = "SELECT * FROM course";
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<MyDataModel> data = new List<MyDataModel>();

            while (reader.Read())
            {
                MyDataModel row = new MyDataModel();
                row.id = (int)reader["courseId"];
                row.courseName = (string)reader["name"];
                row.lecturerName = (string)reader["lecturerName"];
                
                data.Add(row);
            }

            ViewData["data"] = data;

            conn.Close();
        }

        public void OnPost() {

            string connectionStr = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=studentManagement;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionStr);

            int courseId = int.Parse(Request.Form["course"]);
            string studentName = Request.Form["name"];
            string city = Request.Form["city"];

            string insertQuery = "INSERT INTO student(name, city, courseId) VALUES(@name, @city, @courseId)";

            SqlCommand cmd1 = new SqlCommand(insertQuery, conn);
            conn.Open();

            cmd1.Parameters.AddWithValue("@name", studentName);
            cmd1.Parameters.AddWithValue("@city", city);
            cmd1.Parameters.AddWithValue("@courseId", courseId);

            cmd1.ExecuteScalar();

            ViewData["success"] = true;

            conn.Close();
        }
    }
}
