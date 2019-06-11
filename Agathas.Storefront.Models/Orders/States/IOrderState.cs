using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agathas.Storefront.Models.Orders.States
{
    public interface IOrderState
    {
        int Id { get; set; }
        OrderStatus Status { get; }
        bool CanAddProduct();
        void Submit(Order order);
    }    

}
