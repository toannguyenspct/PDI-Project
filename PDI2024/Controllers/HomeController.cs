using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PDI2024.Controllers
{
    public class HomeController : Controller
    {
        //string DBConnection = ConfigurationManager.AppSettings["DBConnection"];
        // Chuỗi kết nối sử dụng tên máy chủ, cổng và tên dịch vụ hoặc SID
        private static readonly string connectionString = ConfigurationManager.AppSettings["DBConnection"];

        //private static readonly string connectionString = "User Id=<your_username>;Password=<your_password>;Data Source=<your_data_source>";
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult yardLayout()
        {
            OracleDBHelper dbHelper = new OracleDBHelper(connectionString);
            var users = dbHelper.SelectUsers();
            string dbuserid = users[0].VEHICLEID;
            string dbpass = users[0].LOCATION;
            string dbpass1 = users[0].REMARK;
            return View();
        }

        [HttpPost]
        public ActionResult yardFullds()
        {
            OracleDBHelper dbHelper = new OracleDBHelper(connectionString);
            var x = dbHelper.getVehicle();
            x.ToString().ToList();
            string json;
            json = JsonConvert.SerializeObject(x);
            return Content(json, "application/json");
        }
        [HttpPost]
        public ActionResult yardFulldsID(string value)
        {
            OracleDBHelper dbHelper = new OracleDBHelper(connectionString);
            var x = dbHelper.getVehicleByID(value);
            if (x != null)
            {
                x.ToString().ToList();
                string json;
                json = JsonConvert.SerializeObject(x);
                return Content(json, "application/json");
            }
            return null;
        }
    }
}