using DAL.Helper;
using DAL.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        private IDatabaseHelper _dbHelper;
        public CategoryRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public CategoryModel GetDataById(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_category_get_by_id", "@CatID", id);
                if(!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<CategoryModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoryModel Create(CategoryModel model)
        {
            var msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_category_create",
                    "@Name", model.Name, "@type", model.type);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<CategoryModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoryModel Update(CategoryModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_category_update",
                "@CatID",model.CatID, "@Name",model.Name, "@type",model.type);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception( msgError);
                }
                return result.ConvertTo<CategoryModel>().FirstOrDefault();
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_category_delete", "@CatID", id);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryModel> Search(int pageIndex, int pageSize, out long total, string Name, string type)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_category_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@Name", Name.Trim(),
                    "@type", type);
                if(!string.IsNullOrEmpty(msgError)) throw new Exception(msgError);

                if (dt.Rows.Count > 0) { total = (long)dt.Rows[0]["RecordCount"]; }

                return dt.ConvertTo<CategoryModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
