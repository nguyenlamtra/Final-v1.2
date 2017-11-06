using COmpStore.FrontEnd.Builder;
using COmpStore.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.FrontEnd.Service
{
    public class CategoryService
    {
        private const string BASE_URI= "http://localhost:2693/api/category";
        public static async Task<List<CategoryModel>> GetAll()
        {
            var response = await HttpRequestFactory.Get(BASE_URI);
            var outputModel = response.ContentAsType<List<CategoryModel>>();
            return outputModel;
        }

        public static async Task<bool> Create(CategoryModel model)
        {
            var response = await HttpRequestFactory.Post(BASE_URI, model);
            if ((int)response.StatusCode == 200){
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> Update(CategoryModel model)
        {
            var response = await HttpRequestFactory.Put(BASE_URI, model);
            if ((int)response.StatusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> Delete(int id)
        {
            var response = await HttpRequestFactory.Delete(BASE_URI+"/"+id);
            if ((int)response.StatusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<CategoryModel> GetById(int id)
        {
            var response = await HttpRequestFactory.Get(BASE_URI + "/" + id);
            return response.ContentAsType<List<CategoryModel>>().FirstOrDefault();
        }
    }
}
