using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllBookedUp.Shared
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
