using COmpStore.FrontEnd.Builder;
using COmpStore.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.FrontEnd.Service
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<bool> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(int id);
        Task<T> GetById(int id);
    }

    public class Service<T> : IService<T> where T : class
    {
        private readonly string URI;

        public Service()
        {
            URI = "admin" + typeof(T).Name.Replace("Model","").ToLower();
        }

        public async Task<List<T>> GetAll()
        {
            var response = await HttpRequestFactory.Get(URI);
            var outputModel = response.ContentAsType<List<T>>();
            return outputModel;
        }

        public async Task<bool> Create(T model)
        {
            var response = await HttpRequestFactory.Post(URI, model);
            if ((int)response.StatusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Update(T model)
        {
            var response = await HttpRequestFactory.Put(URI, model);
            if ((int)response.StatusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var response = await HttpRequestFactory.Delete(URI + "/" + id);
            if ((int)response.StatusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<T> GetById(int id)
        {
            var response = await HttpRequestFactory.Get(URI + "/" + id);
            return response.ContentAsType<T>();
        }
    }
}
