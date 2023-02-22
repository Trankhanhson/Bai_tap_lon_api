using DAL.Helper;
using DAL.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class ProductCatRepository : IProductCatRepository
    {
        private IDatabaseHelper _dbHelper;
        public ProductCatRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public ProductCatModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_productcat_get_by_id", "@ProCatId", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<ProductCatModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductCatModel Create(ProductCatModel model)
        {
            var msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_productcat_create",
                    "@Name", model.Name, "@CatID", model.CatID, "@Image", model.Image);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<ProductCatModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductCatModel Update(ProductCatModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_productcat_update",
                "@ProCatId", model.ProCatId, "@Name", model.Name, "@CatID", model.CatID, "@Image", model.Image);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<ProductCatModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_productcat_delete", "@ProCatId", id);
                if (result != null && !string.IsNullOrEmpty(result.ToString()) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductCatModel> Search(int pageIndex, int pageSize, out long total, string ProductCatName, string CategoryName)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_productcat_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@ProductCatName", ProductCatName.Trim(),
                    "@CategoryName", CategoryName.Trim());

                if (!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

                if (dt.Rows.Count > 0) { total = (long)dt.Rows[0]["RecordCount"]; }

                return dt.ConvertTo<ProductCatModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
