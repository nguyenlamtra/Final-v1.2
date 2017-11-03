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
        public static async Task<List<CategoryModel>> GetList()
        {
            var response = await HttpRequestFactory.Get(BASE_URI);
            var outputModel = response.ContentAsType<List<CategoryModel>>();
            return outputModel;
        }
    }
}
