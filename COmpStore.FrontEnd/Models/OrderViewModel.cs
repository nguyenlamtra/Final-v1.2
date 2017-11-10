using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.FrontEnd.Models
{
    public class OrderViewModel
    {
        public int UserId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<SelectedProduct> SelectedProducts { get; set; }
    }
}
