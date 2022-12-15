using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Git_ProductAPI.Repository
{
    public class BaseReprository
    {
        private readonly IConfiguration _configuration;
        protected BaseReprository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
