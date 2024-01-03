using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PDI2024.Models;
using OfficeOpenXml;

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

        public ActionResult importExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult importExcel(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                List<Vehicle> excelDataList = new List<Vehicle>();
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(file.InputStream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        excelDataList.Add(new Vehicle
                        {
                            VEHICLEID = worksheet.Cells[row, 1].Value.ToString(),
                            LOCATION = worksheet.Cells[row, 2].Value.ToString(),
                            REMARK = worksheet.Cells[row, 3].Value.ToString(),
                            // Map các cột khác nếu cần
                        });
                    }
                }
                return View("importEdit", excelDataList);
            }

            return View();
        }

        public ActionResult importEdit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult importEdit(List<importReceive> editedData)
        {
            if (ModelState.IsValid)
            {
                // Your save logic here

                // Redirect or return a view accordingly
            }

            // If ModelState is not valid, return the view with validation errors
            return View(editedData);
        }


    }
}