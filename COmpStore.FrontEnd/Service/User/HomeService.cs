using COmpStore.FrontEnd.Builder;
using COmpStore.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace COmpStore.FrontEnd.Service.User
{
    public interface IHomeService 
    {
        Task<IEnumerable<CartViewModel>> GetForCartView(int[] productIds);
        Task<bool> SaveOrder(OrderViewModel orderViewModel);
        Task<string> GetToken(LoginViewModel viewModel);
    }

    public class HomeService : IHomeService
    {
        public async Task<IEnumerable<CartViewModel>> GetForCartView(int[] productIds)
        {
            var response = await HttpRequestFactory.Post("home/get-cart", productIds);
            if ((int)response.StatusCode == 200)
            {
                return response.ContentAsType<List<CartViewModel>>();
            }
            else
                return null;
        }

        public async Task<string> GetToken(LoginViewModel viewModel)
        {
            var response = await HttpRequestFactory.Post("authen/test", viewModel);
            return response.StatusCode == HttpStatusCode.OK?response.ContentAsString():null;
            
        }

        public async Task<bool> SaveOrder(OrderViewModel orderViewModel)
        {
            var response = await HttpRequestFactory.Post("home/save-order", orderViewModel);
            if ((int)response.StatusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
