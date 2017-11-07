using COmpStore.Schema;
using COmpStore.Schema.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Services
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private StoreDbContext _context;
        public SubCategoryRepository(StoreDbContext context)
        {
            _context = context;
        }

        public IList<SubCategory> GetAllSubCategories()
        {
            return _context.SubCategories.Include(s => s.Category).ToList();
        }

        public SubCategory GetSingleSubCategory(int id)
        {
            return _context.SubCategories.Include(s => s.Category).Include(s=>s.Products).FirstOrDefault(s => s.Id == id);
        }


        public void Add(SubCategory subCategory)
        {
            _context.SubCategories.Add(subCategory);

        }

        public void Delete(int id)
        {
            SubCategory subCategory = GetSingleSubCategory(id);
            _context.SubCategories.Remove(subCategory);
        }

        public void Update(SubCategory subCategory)
        {
            _context.SubCategories.Update(subCategory);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool Delete(int[] ids)
        {

            try
            {
                foreach (int id in ids)
                {
                    var subCategory = _context.SubCategories.FirstOrDefault(c => c.Id == id);
                    if (subCategory != null)
                    {
                        _context.SubCategories.Remove(subCategory);
                    }
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
    public interface ISubCategoryRepository
    {
        void Add(SubCategory subCategory);
        bool Delete(int[] ids);
        IList<SubCategory> GetAllSubCategories();
        SubCategory GetSingleSubCategory(int id);
        bool Save();
        void Update(SubCategory item);
    }
}
