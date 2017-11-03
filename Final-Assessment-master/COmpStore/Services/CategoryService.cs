using AutoMapper;
using COmpStore.Dto;
using COmpStore.Schema;
using COmpStore.Schema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        bool Delete(int id);
        bool Create(CategoryDto dto);
        bool Update(CategoryDto dto);
    }

    public class CategoryService : ICategoryService
    {
        private StoreDbContext DbContext;
        private IMapper Mapper;

        public CategoryService(StoreDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public bool Create(CategoryDto dto)
        {
            try
            {
                var categoryEntity = Mapper.Map<CategoryEntity>(dto);
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
