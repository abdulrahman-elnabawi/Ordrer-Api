using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderEntities
{
    public class OrderItem :BaseEntity
    {
        public OrderItem()
        {
            
        }

        public OrderItem(decimal price, int quantity, ProductItemOrderd itemOrderd)
        {
            Price = price;
            Quantity = quantity;
            ItemOrderd = itemOrderd;
        }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductItemOrderd ItemOrderd { get; set; }

    }
}
