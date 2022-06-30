using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;


namespace DataAccess
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;
        private static readonly object instanceLock = new object();
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<string> GetAllCategoryName()
        {
            try
            {
                FStoreDBContext context = new FStoreDBContext();
                var list = from category in context.Categories
                           select category.CategoryName;
                return list;


            } catch (Exception ex)
            {
                throw new Exception("Error in get category: " + ex.Message);
            }
        }


        public IEnumerable<int> GetAllCategoryID()
        {
            try
            {
                FStoreDBContext context = new FStoreDBContext();
                var list = from category in context.Categories
                           select category.CategoryId;
                return list;


            }
            catch (Exception ex)
            {
                throw new Exception("Error in get category: " + ex.Message);
            }
        }
        public int GetCategoryByName(string name)
        {
            try
            {
                throw new Exception(name);
                FStoreDBContext context = new FStoreDBContext();
                var cateId = context.Categories.SingleOrDefault(cate => cate.CategoryName == name).CategoryId;
                return cateId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in get category: " + ex.Message);
            }
        }
    }
}
