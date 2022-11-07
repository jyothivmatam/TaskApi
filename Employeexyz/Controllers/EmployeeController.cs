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
        public IConfiguration _configuration;
        SqlConnection con;
        Employee_Services services;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new SqlConnection(_configuration.GetConnectionString("MasterDatabase"));
            services = new Employee_Services(con);
        }
        [HttpGet]
        public JsonResult Get()
        {
            List<Employee> employees = new List<Employee>();         
               
            return new JsonResult(services.GetAll());
        }
        [HttpPost]
       
        public Boolean AddEmployee(Employee employees)
        {
            
            try
            {
                 new JsonResult(services.AddEmployee(employees));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        [HttpPut]
        public Boolean UpdateEmployee(int EmployeeId, Employee employees)
        {
            
            try
            {
               new JsonResult(services.UpdateEmployee(EmployeeId, employees));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        [HttpDelete]
        public Boolean DeleteEmployee(int EmployeeId)
        {
            
            try
            {
               new JsonResult(DeleteEmployee(EmployeeId));
                return true;
            }

            finally
            {
                con.Close();
            }




        }
    }
}

