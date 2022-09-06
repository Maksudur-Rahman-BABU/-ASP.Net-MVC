using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Work_01.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            try
            {
                string str = @"Data Source=.;Initial Catalog=BABU;Integrated Security=True";
                SqlConnection con = new SqlConnection(str);
                SqlDataAdapter ada = new SqlDataAdapter();
                SqlCommand command = con.CreateCommand();
                command.CommandText = "Select * from [Students] s inner join Departments d on s.DepartmentId=d.DepartmentId inner join Institutes i on s.InstituteId=i.InstituteId";
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "Students");
                ReportDocument rd = new ReportDocument();
                string rptPath = System.Web.HttpContext.Current.Server.MapPath("~/CrystalReport/rptStudentReport.rpt");
                rd.Load(rptPath);
                rd.SetDataSource(ds);
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Flush();
                rd.Close();
                rd.Dispose();
                return File(stream, System.Net.Mime.MediaTypeNames.Application.Pdf);
            }
            catch (Exception ex)
            {
                return Content("<h2>Error:" + ex.Message + "</h2>", "text/html");
            }
        }
    }
}