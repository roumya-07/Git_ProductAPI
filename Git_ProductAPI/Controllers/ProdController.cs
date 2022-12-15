using Git_ProductAPI.Models;
using Git_ProductAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Git_ProductAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProdService _prodservice;
        public ProductController(IProdService prodservice)
        {
            _prodservice = prodservice;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProduct()
        {
            return await _prodservice.GetAllProduct();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var prod = await _prodservice.GetProductById(id);

            if (prod == null)
            {
                return NotFound();
            }
            return prod;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> InsertUpdateProduct(int id, Product prod)
        {
            if (id != prod.productid)
            {
                return BadRequest();
            }
            try
            {
                await _prodservice.Insert(prod);

                return CreatedAtAction("GetAllProduct", new { id = prod.productid }, prod);
            }

            catch (Exception ex)
            {
                if (GetProductById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var prod = await _prodservice.GetProductById(id);
            if (prod == null)
            {
                return NotFound();
            }
            await _prodservice.Delete(id);
            return prod;
        }
    }
    
}
