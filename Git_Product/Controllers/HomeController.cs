using Git_Product.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Git_Product.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        Uri baseAdd = new Uri("http://localhost:41000/api");

        HttpClient client;
        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
            client = new HttpClient();
            client.BaseAddress = baseAdd;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> lstcat = new List<Category>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Category").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                lstcat = JsonConvert.DeserializeObject<List<Category>>(data);
                lstcat.Insert(0, new Category { catid = 0, catdesc = "Select One" });
                ViewBag.Category = lstcat;
            }
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Product prod)
        //{
        //    string data = JsonConvert.SerializeObject(prod);
        //    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Product/" + prod.productid, content).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        [HttpPost]
        public IActionResult SaveUpdate(int pid, Product prd)
        {
            string[] files = prd.productimage.Split('\\');
            prd.productimage = "prodimage/" + files[files.Length - 1];
            string data = JsonConvert.SerializeObject(prd);
            HttpResponseMessage response;
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            response = client.PutAsync(client.BaseAddress + "/Product/" + prd.productid, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }
        public async Task<JsonResult> GetProducts()
        {
            string data = null;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Product").Result;
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
            }
            return Json(data);
        }
        public async Task<JsonResult> SubCat_Bind(int catid)
        {
            string data = null;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/SubCategory/" + catid).Result;
            List<SelectListItem> scalist = new List<SelectListItem>();
            if (response.IsSuccessStatusCode)
            {
                data = response.Content.ReadAsStringAsync().Result;
                var lstscat = JsonConvert.DeserializeObject<List<SubCategory>>(data);
                foreach (SubCategory dr in lstscat)
                {
                    scalist.Add(new SelectListItem { Text = dr.subcatdesc, Value = dr.subcatid.ToString() });
                }
            }
            var jsonres = JsonConvert.SerializeObject(scalist);
            return Json(jsonres);
        }
        [HttpPost]
        public IActionResult UploadImage(IFormFile MyUploader)
        {
            if (MyUploader != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "prodimage");
                string filePath = Path.Combine(uploadsFolder, MyUploader.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    MyUploader.CopyTo(fileStream);
                }
                return new ObjectResult(new { status = "success" });
            }
            return new ObjectResult(new { status = "fail" });

        }
        public async Task<JsonResult> Edit(int productid)
        {
            Product productlist = new Product();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Product/" + productid).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                productlist = JsonConvert.DeserializeObject<Product>(data);
            }
            var jsonres = JsonConvert.SerializeObject(productlist);
            return Json(jsonres);
        }
        public int Delete(int productid)
        {
            Product productlist = new Product();
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Product/" + productid).Result;
            if (response.IsSuccessStatusCode)
            {
                return 1;

            }
            return 0;
        }
    }
}
