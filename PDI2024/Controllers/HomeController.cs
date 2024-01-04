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
            var data = dbHelper.getVehicle();
            data.ToString().ToList();
            string json;
            json = JsonConvert.SerializeObject(data);
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

                    for (int row = 8; row <= rowCount; row++)
                    {
                        var vehicleIdCell = worksheet.Cells[row, 3].Value;
                        var locationCell = worksheet.Cells[row, 8].Value;
                        if (vehicleIdCell == null || locationCell == null)
                        {
                            // Nếu bất kỳ ô nào ở cột 3 hoặc 8 là null, dừng lại
                            break;
                        }
                        excelDataList.Add(new Vehicle
                        {
                            VEHICLEID = vehicleIdCell.ToString(),
                            LOCATION = locationCell.ToString(),
                            REMARK = (worksheet.Cells[row, 14].Value ?? string.Empty).ToString(),
                            //REMARK = (worksheet.Cells[row, 14].Value.ToString() == null || worksheet.Cells[row, 14].Value.ToString() == "") ? worksheet.Cells[row, 14].Value.ToString() : "",
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
        public ActionResult importEdit(List<Vehicle> editedData)
        {

            if (ModelState.IsValid)
            {
                OracleDBHelper dbHelper = new OracleDBHelper(connectionString);
                foreach (var dataItem in editedData)
                {

                    var VEHICLEID = dataItem.VEHICLEID;
                    var LOCATION = dataItem.LOCATION;
                    var REMARK = dataItem.REMARK;
                    dbHelper.insertVehicle(VEHICLEID, LOCATION, REMARK,1);
                    // Perform actions or validations as needed
                }
                // Your save logic here
                return View(editedData);
                // Redirect or return a view accordingly
            }

            // If ModelState is not valid, return the view with validation errors
            return View(editedData);
        }


    }
}