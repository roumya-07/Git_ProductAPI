using Git_ProductAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Git_ProductAPI.Service
{
    public interface IProdService
    {
        public Task<List<Product>> GetAllProduct();
        public Task<List<Category>> GetAllCategory();
        public Task<List<SubCategory>> GetAllSubCategory(int catid);
        public Task<Product> GetProductById(int pid);
        public Task<int> Insert(Product pr);
        public Task<int> Delete(int pid);
    }
}
