using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.Interfaces
{
    public interface ICategoryRepository
    {
        CategoryModel GetDataById(int id);
        CategoryModel Create(CategoryModel model);
        CategoryModel Update(CategoryModel model);
        bool Delete(int id);

        List<CategoryModel> Search(int pageIndex, int pageSize, out long total, string Name,string type);
    }
}
