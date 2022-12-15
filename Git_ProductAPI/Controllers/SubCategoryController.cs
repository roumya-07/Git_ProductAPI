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
    public class SubCategoryController : Controller
    {
        private readonly IProdService _prodservice;
        public SubCategoryController(IProdService prodservice)
        {
            _prodservice = prodservice;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<SubCategory>>> GetAllSubCategory(int id)
        {
            return await _prodservice.GetAllSubCategory(id);
        }
    }
}
