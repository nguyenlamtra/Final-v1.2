using COmpStore.FrontEnd.Builder;
using COmpStore.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.FrontEnd.Services
{
    public class ProductService
    {
        private const string BASE_URI = "http://localhost:2693/api/product";
        public static async Task<List<ProductModel>> GetAll()
        {
            var response = await HttpRequestFactory.Get(BASE_URI);
            var outputModel = response.ContentAsType<List<ProductModel>>();
            return outputModel;
        }

        public static async Task<bool> Create(ProductModel model)
        {
            var response = await HttpRequestFactory.Post(BASE_URI, model);
            if ((int)response.StatusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> Update(ProductModel model)
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
            var response = await HttpRequestFactory.Delete(BASE_URI + "/" + id);
            if ((int)response.StatusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<ProductModel> GetById(int id)
        {
            var response = await HttpRequestFactory.Get(BASE_URI + "/" + id);
            return response.ContentAsType<ProductModel>();
        }
    }
}
