using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Agathas.Storefront.Infrastructure.Helpers;
using Agathas.Storefront.Models;
using Agathas.Storefront.Models.Basket;
using Agathas.Storefront.Models.Categories;
using Agathas.Storefront.Models.Customers;
using Agathas.Storefront.Models.Orders;
using Agathas.Storefront.Models.Orders.States;
using Agathas.Storefront.Models.Products;
using Agathas.Storefront.Models.Shipping;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services {
  public class AutoMapperBootStrapper : Profile {
    public AutoMapperBootStrapper() {
      // Product Title
      CreateMap<ProductTitle, ProductSummaryView>();
      CreateMap<ProductTitle, ProductView>();
      CreateMap<Product, ProductSummaryView>();
      CreateMap<Product, ProductSizeOption>();

      // Category
      CreateMap<Category, CategoryView>();

      // IProductAttribute
      CreateMap<IProductAttribute, Refinement>();

      // Basket
      CreateMap<DeliveryOption, DeliveryOptionView>();
      CreateMap<BasketItem, BasketItemView>();
      CreateMap<Basket, BasketView>();

      // Customer
      CreateMap<Customer, CustomerView>();
      CreateMap<DeliveryAddress, DeliveryAddressView>();

      // Orders
      CreateMap<Order, OrderView>();
      CreateMap<OrderItem, OrderItemView>();
      CreateMap<Address, DeliveryAddressView>();
      CreateMap<Order, OrderSummaryView>()
          .ForMember(dest => dest.IsSubmitted,
                  opt => opt.MapFrom(s => s.Status == OrderStatus.Submitted ? true : false));  
    }
  }
  public class MoneyFormatter {
    public string Convert(decimal source) {
      return source.FormatMoney();
    }
  }
}
