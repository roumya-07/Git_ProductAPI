using Git_ProductAPI.Models;
using Git_ProductAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Git_ProductAPI.Service
{
    public class ProdService : IProdService
    {
        private readonly IProdReprository _prodrepo;
        public ProdService(IProdReprository prodrepo)
        {
            _prodrepo = prodrepo;
        }
        public async Task<int> Delete(int pid)
        {
            return await _prodrepo.Delete(pid);
        }

        public async Task<List<Category>> GetAllCategory()
        {
            return await _prodrepo.GetAllCategory();
        }

        public async Task<List<Product>> GetAllProduct()
        {
            return await _prodrepo.GetAllProduct();
        }

        public async Task<List<SubCategory>> GetAllSubCategory(int catid)
        {
            return await _prodrepo.GetAllSubCategory(catid);
        }

        public async Task<Product> GetProductById(int pid)
        {
            return await _prodrepo.GetProductById(pid);
        }

        public async Task<int> Insert(Product pr)
        {
            return await _prodrepo.Insert(pr);
        }
    }
}
