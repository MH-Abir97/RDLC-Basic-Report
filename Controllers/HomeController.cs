using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDLC.Data;
using RDLC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RDLC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly SalaryService _salaryService;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env, AppDbContext context)
        {
            _logger = logger;
            _env = env;
            _context = context;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

       
        public DataTable GetEmployeeSalaryInfo()
        {
            string connnectionStr = "Data Source=192.168.4.9; Initial Catalog=Retail_Dev; User Id=sa; Password=sql@123;";
            var dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connnectionStr))
            {
                SqlCommand cmd = new SqlCommand("ad_Employee_Temp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return dt;
         
        }

        public IActionResult EmployeeSalaryInfo()
        {
         
            var result = _context.Employee.FromSqlRaw("ad_Employee_Temp")
                              .Select(e => new { e.EmployeeId, e.FullName, e.DepartmentId,e.DepartmentName ,e.BasicSalary})
                              .AsEnumerable();

            var emp = result.ToList();

            //var dt = new DataTable();
         
            //dt = GetEmployeeSalaryInfo();

            string mintype = "";
            int extension = 1;
            var path = $"{_env.WebRootPath}\\Reports\\rpt_hr_SalarySheet.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm1", "RDLC Report");
            //parameters.Add("prm2", DateTime.Now.ToString("dd-MMM-yyyy"));
            parameters.Add("prm3", "Employee Salary Report");

            LocalReport localReport = new LocalReport(path);

            localReport.AddDataSource("dsEmployeeSalary", emp);

            var res = localReport.Execute(RenderType.Pdf, extension, parameters, mintype);
            return File(res.MainStream, "application/pdf");
        }

        /// Sub Report 

        public IActionResult SubReportPrint()
        {
            var dt = new DataTable();
            dt = GetEmployeeList();

        

            string mintype = "";
            int extension = 1;
            var path = $"{_env.WebRootPath}\\Reports\\rptEmployeeParent.rdlc";
          

            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("prm1", "Employee Report");

            LocalReport localReport = new LocalReport(path);
     
            localReport.AddDataSource("dsEmployeeParent", dt);
        

           
            var res = localReport.Execute(RenderType.Pdf, extension);
            return File(res.MainStream, "application/pdf");

        }


        public DataTable GetEmployeeList()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpId");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("Department");
            dt.Columns.Add("Designation");
            DataRow row;
            for (int i = 101; i <= 120; i++)
            {
                row = dt.NewRow();
                row["EmpId"] = i;
                row["EmpName"] = "Mr. Abir"+i;
                row["Department"] ="Software";
                row["Designation"] ="Asso. Software Eng";
                dt.Rows.Add(row);
            }
            return dt;
        }


        public DataTable GetEmployeeDetails()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpId");
            dt.Columns.Add("Details");
         
            DataRow row;
            for (int i = 101; i <= 120; i++)
            {
                for (int j = 0; j <=3; j++)
                {
                    row = dt.NewRow();
                    row["EmpId"] = i;
                    row["Details"] = "Child -" + j;
                    dt.Rows.Add(row);
                }
               
            }
            return dt;
        }

    }
}
