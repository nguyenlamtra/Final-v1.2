using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Models
{
    public class OrderViewModel
    {
        public List<SelectedProductViewModel> SelectedProducts { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
