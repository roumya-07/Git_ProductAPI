using Git_Product.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Git_Product.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAdd = new Uri("http://localhost:41000/api");

        HttpClient client;
        public HomeController()
        {
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

        [HttpPost]
        public async Task<IActionResult> Create(Product prod)
        {
            string data = JsonConvert.SerializeObject(prod);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Product/" + prod.productid, content).Result;
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
