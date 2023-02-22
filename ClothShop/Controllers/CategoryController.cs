using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ClothShop.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryBusiness _categoryBusiness;
        public CategoryController(ICategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }

        [Route("get-category-by-id/{id}")]
        [HttpGet]
        public CategoryModel GetDataById(int id)
        {
            return _categoryBusiness.GetDataById(id);
        }

        [Route("create_category")]
        [HttpPost]
        public CategoryModel Create(CategoryModel model)
        {
            return _categoryBusiness.Create(model);
        }

        [Route("update_category")]
        [HttpPost]
        public CategoryModel Update(CategoryModel model)
        {
            return _categoryBusiness.Update(model);
        }

        [Route("delete_category")]
        [HttpGet]
        public bool Delete(int id)
        {
            return _categoryBusiness.Delete(id);
        }

        [Route("search-category")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string Name = "";
                if (formData.Keys.Contains("Name") && !string.IsNullOrEmpty(Convert.ToString(formData["Name"]))) { Name = Convert.ToString(formData["Name"]); }
                string type = "";
                if (formData.Keys.Contains("type") && !string.IsNullOrEmpty(Convert.ToString(formData["type"]))) { type = Convert.ToString(formData["type"]); }
                long total = 0;
                var data = _categoryBusiness.Search(page, pageSize, out total, Name,type);
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
