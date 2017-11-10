using COmpStore.FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.FrontEnd.Helper
{
    public static class SessionExtensions
    {
        public static List<SelectedProduct>
           GetSession(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return new List<SelectedProduct>();
            }

            return JsonConvert.DeserializeObject<List<SelectedProduct>>(data);
        }

        public static void SetSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static void SetSelectedProducts(this ISession session, List<SelectedProduct> selectedProducts)
        {
            session.SetString("selectedProducts", JsonConvert.SerializeObject(selectedProducts));
        }

        public static List<SelectedProduct>
           GetSelectedProducts(this ISession session)
        {
            var data = session.GetString("selectedProducts");
            if (data == null)
            {
                return new List<SelectedProduct>();
            }

            return JsonConvert.DeserializeObject<List<SelectedProduct>>(data);
        }
    }
}
