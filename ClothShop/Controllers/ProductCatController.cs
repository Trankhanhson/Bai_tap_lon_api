using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.IO;

namespace ClothShop.Controllers
{
    public class ProductCatController : Controller
    {
        private IProductCatBusiness productCatBusiness;
        public ProductCatController(IProductCatBusiness productCatBusiness)
        {
            this.productCatBusiness = productCatBusiness;
        }

        [Route("get-procat-by-id/{id}")]
        [HttpGet]
        public ProductCatModel GetDataById(int id)
        {
            return productCatBusiness.GetDataById(id);
        }

        [Route("create_productcat")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCatModel model)
        {
            
            //handle image
            if (model.fileImage.Length >0)
            {
                var fileName = DateTime.Now.Ticks.ToString() + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductCat", fileName);
                
                using (var stream = System.IO.File.Create(path))
                {
                    await model.fileImage.CopyToAsync(stream);
                }
                model.Image = "/ProductCat/" + fileName;
            }
            else
            {
                model.Image = "";
            }
            var result = productCatBusiness.Create(model);
            return Ok(result);
        }

        [Route("update_productcat")]
        [HttpPost]
        public ProductCatModel Update(ProductCatModel model)
        {
            return productCatBusiness.Update(model);
        }

        [Route("delete_productcat")]
        [HttpGet]
        public bool Delete(int id)
        {
            return productCatBusiness.Delete(id);
        }

        [Route("search-productcat")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string ProductCatName = "";
                if (formData.Keys.Contains("ProductCatName") && !string.IsNullOrEmpty(Convert.ToString(formData["ProductCatName"]))) { ProductCatName = Convert.ToString(formData["ProductCatName"]); }
                string CategoryName = "";
                if (formData.Keys.Contains("CategoryName") && !string.IsNullOrEmpty(Convert.ToString(formData["CategoryName"]))) { CategoryName = Convert.ToString(formData["CategoryName"]); }
                long total = 0;
                var data = productCatBusiness.Search(page, pageSize, out total, ProductCatName, CategoryName);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
