using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SensorDataWeb.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Data.SqlClient;

namespace SensorDataWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            SqlServerConnHelp sqlConnManager = new SqlServerConnHelp();
            SqlConnection sqlConnection = sqlConnManager.GetConnection(@"DELL-SVR\SQLEXPRESS", "SensorData", "jdw", "asdf");
            //SqlConnection sqlConnection = sqlConnManager.GetConnection(@"LAPTOP-AD2UF7HI\SQLEXPRESS", "SensorData", "jdw", "asdf");
            string query = "Select * from SensorData";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            var reader = cmd.ExecuteReader();
            List<SensorDataFields> db = new List<SensorDataFields>();

            while(reader.Read())
            {
                SensorDataFields row = new SensorDataFields();
                row.Id = reader.GetInt32(0);
                row.UniqueId = reader.GetString(1).Trim();
                row.TransmissionId = reader.GetString(2).Trim();
                row.IpAddress = reader.GetString(3).Trim();
                row.date = reader.GetDateTime(4);
                db.Add(row);
            }

            reader.Close();
            cmd.Dispose();
            sqlConnection.Close();
            sqlConnection.Dispose();

            ViewData["SensorDataFields"] = db;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
