using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductCatModel
    {
        public int ProCatId { get; set; }
        public string Name { get; set; }
        public int CatID { get; set; }
        public string Image { get; set; }

        public string CategoryName { get; set; }
        public string CategoryType { get; set; }

        public IFormFile fileImage { get; set; }
    }
}
