using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private ICategoryRepository _res;
        public CategoryBusiness(ICategoryRepository categoryRepository)
        {
            _res = categoryRepository;
        }

        public CategoryModel GetDataById(int id)
        {
            return _res.GetDataById(id);
        }

        public CategoryModel Create(CategoryModel model)
        {
            return _res.Create(model);
        }
        public CategoryModel Update(CategoryModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }

        public List<CategoryModel> Search(int pageIndex, int pageSize, out long total, string Name, string type)
        {
            return _res.Search(pageIndex, pageSize,out total, Name, type);
        }
    }
}
