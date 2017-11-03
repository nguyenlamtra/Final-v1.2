using AutoMapper;
using COmpStore.Dto;
using COmpStore.Schema;
using COmpStore.Schema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryDto> GetAll();
        bool Delete(int id);
        bool Create(CategoryDto dto);
        bool Update(CategoryDto dto);
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

        public bool Delete(int id)
        {
            try
            {
                var category = DbContext.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    DbContext.Categories.Remove(category);
                    DbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return Mapper.Map<IEnumerable<CategoryDto>>(DbContext.Categories);
        }

        public bool Update(CategoryDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
