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
    public interface IPublisherRepository
    {
        IEnumerable<PublisherDto> GetAll();
        bool Delete(int[] ids);
        bool Create(PublisherDto dto);
        bool Update(PublisherDto dto);
        PublisherDto GetById(int Id);
    }

    public class PublisherRepository : IPublisherRepository
    {
        private StoreDbContext DbContext;

        public PublisherRepository(StoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public bool Create(PublisherDto dto)
        {
            try
            {
                var publisher = Mapper.Map<Publisher>(dto);
                DbContext.Publishers.Add(publisher);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool Delete(int[] ids)
        {
            try
            {
                foreach (int id in ids)
                {
                    var publisher = DbContext.Publishers.FirstOrDefault(c => c.Id == id);
                    if (publisher != null)
                    {
                        DbContext.Publishers.Remove(publisher);
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

        public IEnumerable<PublisherDto> GetAll()
        {
            return Mapper.Map<IEnumerable<PublisherDto>>(DbContext.Publishers.Include(x=>x.Products));
        }

        public PublisherDto GetById(int id)
        {
            var publisher = DbContext.Publishers.Include(x => x.Products).SingleOrDefault(x => x.Id == id);
            if (publisher != null)
                return Mapper.Map<PublisherDto>(publisher);
            else
                return null;
        }

        public bool Update(PublisherDto dto)
        {
            try
            {
                var publiser = Mapper.Map<Publisher>(dto);
                DbContext.Entry(publiser).State = EntityState.Modified;
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
