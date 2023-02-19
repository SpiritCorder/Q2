using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentManagementSystem.Pages
{
    public class CourseAddModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost() {
            string courseName = Request.Form["name"];
            string lecturerName = Request.Form["lecturer"];

            string connectionStr = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=studentManagement;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionStr);

            string query = "INSERT INTO course (name, lecturerName) VALUES(@name, @lecturerName)";

            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();

            cmd.Parameters.AddWithValue("@name", courseName);
            cmd.Parameters.AddWithValue("@lecturerName", lecturerName);

            cmd.ExecuteScalar();

            ViewData["success"] = true;
          
            conn.Close();


        }
    }
}
