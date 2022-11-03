using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;
//using Employeexyz.Model;


namespace Employeexyz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("MasterDatabase"))) 
            //using (SqlConnection con = new SqlConnection(confgjs))

            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Employee";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                con.Open();
                adapter.Fill(dataTable);
                con.Close();
                foreach (DataRow dr in dataTable.Rows)
                {
                    employees.Add(new Employee
                    {
                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                        EmployeeName = dr["EmployeeName"].ToString(),
                        EmployeeEmail = dr["EmployeeEmail"].ToString(),
                    });
                }
            }
            return new JsonResult(employees);
        }




    }
}

