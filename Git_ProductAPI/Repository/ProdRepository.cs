using Dapper;
using Git_ProductAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Git_ProductAPI.Repository
{
    public class ProdReprository : BaseReprository, IProdReprository
    {
        public ProdReprository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<List<Product>> GetAllProduct()
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@mode", "FillTable");
            var lstprod = cn.Query<Product>("sp_ProductCatsubcatFile", param, commandType: CommandType.StoredProcedure).ToList();
            return lstprod;
        }
        public async Task<Product> GetProductById(int pid)
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@productid", pid);
            param.Add("@mode", "SelectOne");
            var lstprod = cn.Query<Product>("sp_ProductCatsubcatFile", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            cn.Close();
            return lstprod;
        }
        public async Task<int> Insert(Product pr)
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@productid", pr.productid);
            param.Add("@productname", pr.productname);
            param.Add("@productdesc", pr.productdesc);
            param.Add("@catid", pr.catid);
            param.Add("@subcatid", pr.subcatid);
            param.Add("@productprice", pr.productprice);
            param.Add("@productqty", pr.productqty);
            param.Add("@productimage", pr.productimage);
            param.Add("@mode", "insUp");
            int x = cn.Execute("sp_ProductCatsubcatFile", param, commandType: CommandType.StoredProcedure);
            cn.Close();
            return x;
        }
        public async Task<int> Delete(int productid)
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@productid", productid);
            param.Add("@mode", "delete");
            int x = cn.Execute("sp_ProductCatsubcatFile", param, commandType: CommandType.StoredProcedure);
            cn.Close();
            return x;
        }

        public async Task<List<Category>> GetAllCategory()
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@mode", "bindCat");
            var lstprod = cn.Query<Category>("sp_ProductCatsubcatFile", param, commandType: CommandType.StoredProcedure).ToList();
            return lstprod;
        }

        public async Task<List<SubCategory>> GetAllSubCategory(int catid)
        {
            var cn = CreateConnection();
            if (cn.State == ConnectionState.Closed) cn.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@catid", catid);
            param.Add("@mode", "bindsubCat");
            var lstprod = cn.Query<SubCategory>("sp_ProductCatsubcatFile", param, commandType: CommandType.StoredProcedure).ToList();
            return lstprod;
        }
    }
}
