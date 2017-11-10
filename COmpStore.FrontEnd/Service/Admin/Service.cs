using COmpStore.FrontEnd.Builder;
using COmpStore.FrontEnd.Models;
using COmpStore.FrontEnd.Helper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace COmpStore.FrontEnd.Service.Admin
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Create(T model);
        Task<T> Update(T model);
        //Task<bool> Delete(int id);
        Task<bool> Delete(int[] ids);
        Task<T> GetById(int id);
    }

    public class Service<T> : IService<T> where T : class
    {
        private readonly string URI;

        public Service()
        {
            URI = "admin" + typeof(T).Name.Replace("Model", "").ToLower();
        }

        public async Task<List<T>> GetAll()
        {
            var response = await HttpRequestFactory.Get(URI);
            var outputModel = response.ContentAsType<List<T>>();
            return outputModel;
        }

        public async Task<T> Create(T model)
        {
            var response = await HttpRequestFactory.Post(URI, model);
            if ((int)response.StatusCode == 200)
                return response.ContentAsType<T>();
            return null;
        }

        public async Task<T> Update(T model)
        {
            var response = await HttpRequestFactory.Put(URI, model);
            if ((int)response.StatusCode == 200)
                return response.ContentAsType<T>();
            return null;
        }

        public async Task<T> GetById(int id)
        {
            var response = await HttpRequestFactory.Get(URI + "/" + id);
            if ((int)response.StatusCode == 200)
                return response.ContentAsType<T>();
            else
                return null;
        }

        public async Task<bool> Delete(int[] ids)
        {
            var response = await HttpRequestFactory.Delete(URI, ids);

            if ((int)response.StatusCode == 204)
                return true;
            else
                return false;
        }

        //public async Task<bool> Delete(int id)
        //{
        //    var response = await HttpRequestFactory.Delete(URI + "/" + id);

        //    if ((int)response.StatusCode == 204)
        //        return true;
        //    else
        //        return false;
        //}
    }
}
