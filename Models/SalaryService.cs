using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RDLC.Models
{
    public class SalaryService
    {
        string connnectionStr = "Data Source=192.168.4.9; Initial Catalog=Retail_Dev; User Id=sa; Password=sql@123;";
        public DataTable GetEmployeeSalaryInfo()
        {
            var dt = new DataTable();
            using (SqlConnection con =new SqlConnection(connnectionStr))
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
    }
}
