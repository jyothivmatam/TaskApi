using System.ComponentModel.DataAnnotations;

namespace Employeexyz
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; } 
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeEmail { get; set; } = string.Empty;
    }
}
