using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;
//using Employeexyz.Model;


namespace Employeexyz.Controllers
{
    
    public class Employee_Services 
    {
        
         SqlConnection con;

        public Employee_Services(SqlConnection sqlConnection)
        {
           con = sqlConnection;
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            List<Employee> employees = new List<Employee>();


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
            return new JsonResult(employees);
        }
        [HttpPost]

        public Boolean AddEmployee(Employee employees)
        {

            try
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Employee(EmployeeId,EmployeeName,EmployeeEmail) values(" + employees.EmployeeId + ",'" + employees.EmployeeName + "','" + employees.EmployeeEmail + "')";
                con.Open();
                cmd.ExecuteNonQuery();
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
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update Employee Set EmployeeName='" + employees.EmployeeName + "', EmployeeEmail='" + employees.EmployeeEmail + "' Where(EmployeeId= " + EmployeeId + ");";
                con.Open();
                cmd.ExecuteNonQuery();
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
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from Employee Where(EmployeeId=" + EmployeeId + ");";
                con.Open();
                cmd.ExecuteNonQuery();
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
    }
}


