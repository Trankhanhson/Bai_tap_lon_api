using BLL.Interfaces;
using DAL.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductCatBusiness: IProductCatBusiness
    {
        private IProductCatRepository _res;
        public ProductCatBusiness(IProductCatRepository res)
        {
            _res = res;
        }

        public ProductCatModel GetDataById(int id)
        {
            return _res.GetDataById(id);
        }
        public ProductCatModel Create(ProductCatModel model)
        {
            return _res.Create(model);
        }
        public ProductCatModel Update(ProductCatModel model)
        {
            return _res.Update(model);
        }
        public bool Delete(int id)
        {
            return _res.Delete(id);
        }

        public List<ProductCatModel> Search(int pageIndex, int pageSize, out long total, string ProductCatName, string CategoryName)
        {
            return _res.Search(pageIndex, pageSize, out total, ProductCatName, CategoryName);
        }
    }
}
