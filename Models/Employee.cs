using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDLC.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public int DepartmentId { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime JoiningDate { get; set; }
        public int DesignationId { get; set; }
        public decimal BasicSalary { get; set; }
    }
}
