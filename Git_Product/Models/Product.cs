using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Git_Product.Models
{
    public class Product
    {
        [Key]
        public int productid { get; set; } = 0;

        public string productname { get; set; } = null;
        public string productdesc { get; set; } = null;
        public decimal? productprice { get; set; }
        public int? productqty { get; set; }
        public string productimage { get; set; } = null;
        public int catid { get; set; } = 0;

        public int subcatid { get; set; } = 0;



        [NotMapped]
        public string catdesc { get; set; } = null;

        [NotMapped]
        public string subcatdesc { get; set; } = null;
    }
    public class Category
    {
        [Key]
        public int catid { get; set; } = 0;
        public string catdesc { get; set; } = null;
    }
    public class SubCategory
    {
        [Key]
        public int subcatid { get; set; } = 0;
        public int catid { get; set; } = 0;
        public string subcatdesc { get; set; } = null;
    }
}
