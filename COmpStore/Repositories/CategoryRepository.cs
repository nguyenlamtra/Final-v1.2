using AutoMapper;
using COmpStore.Dto;
using COmpStore.Schema;
using COmpStore.Schema.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryDto> GetAll();
        // bool Delete(int id);
        bool Delete(int[] ids);
        bool Create(CategoryDto dto);
        bool Update(CategoryDto dto);
        CategoryDto GetById(int id);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private StoreDbContext DbContext;

        public CategoryRepository(StoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public bool Create(CategoryDto dto)
        {
            try
            {
                var categoryEntity = Mapper.Map<Category>(dto);
                DbContext.Categories.Add(categoryEntity);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        //public bool Delete(int id)
        //{
        //    try
        //    {
        //        var category = DbContext.Categories.FirstOrDefault(c => c.Id == id);
        //        if (category != null)
        //        {
        //            DbContext.Categories.Remove(category);
        //            DbContext.SaveChanges();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        return false;
        //    }
        //}

        public bool Delete(int[] ids)
        {

            try
            {
                foreach (int i in ids)
                {
                    var category = DbContext.Categories.FirstOrDefault(c => c.Id == i);
                    if (category != null)
                    {
                        DbContext.Categories.Remove(category);
                    }
                }
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public IEnumerable<CategoryDto> GetAll()
        {
<<<<<<< HEAD
            return Mapper.Map<IEnumerable<CategoryDto>>(DbContext.Categories.Include(x=>x.SubCategories));
=======
            return Mapper.Map<IEnumerable<CategoryDto>>(DbContext.Categories.Include(x => x.SubCategories));

>>>>>>> 1d0bfa58f7de88ebda201218ff4bf2506565f1b2
        }

        public CategoryDto GetById(int id)
        {
            var category = DbContext.Categories.Include(x => x.SubCategories).ThenInclude(x => x.Products).SingleOrDefault(x => x.Id == id);
            if (category != null)
                return Mapper.Map<CategoryDto>(category);
            else
                return null;
        }

        public bool Update(CategoryDto dto)
        {
            try
            {
                var category = Mapper.Map<Category>(dto);
                //DbContext.Categories.Attach(category);
                DbContext.Entry(category).State = EntityState.Modified;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }
    }
}
