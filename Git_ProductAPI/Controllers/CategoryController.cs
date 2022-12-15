using Git_ProductAPI.Models;
using Git_ProductAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Git_ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IProdService _prodservice;
        public CategoryController(IProdService prodservice)
        {
            _prodservice = prodservice;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategory()
        {
            return await _prodservice.GetAllCategory();
        }
    }
}
