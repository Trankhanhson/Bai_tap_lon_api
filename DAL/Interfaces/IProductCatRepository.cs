﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductCatRepository
    {
        ProductCatModel GetDataById(int id);
        ProductCatModel Create(ProductCatModel model);
        ProductCatModel Update(ProductCatModel model);
        bool Delete(int id);
        List<ProductCatModel> Search(int pageIndex, int pageSize, out long total, string ProductCatName, string CategoryName);
    }
}
