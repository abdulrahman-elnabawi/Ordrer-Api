﻿using Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.OrderService.Dto
{
    public class OrderItemDto
    {
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }



    }
}
