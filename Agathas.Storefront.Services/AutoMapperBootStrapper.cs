using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Agathas.Storefront.Infrastructure.Helpers;
using Agathas.Storefront.Model;
using Agathas.Storefront.Model.Basket;
using Agathas.Storefront.Model.Categories;
using Agathas.Storefront.Model.Customers;
using Agathas.Storefront.Model.Orders;
using Agathas.Storefront.Model.Orders.States;
using Agathas.Storefront.Model.Products;
using Agathas.Storefront.Model.Shipping;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services {
  public class AutoMapperBootStrapper {
    public static void ConfigureAutoMapper() {
      Mapper.Initialize(cfg => {
        cfg.CreateMissingTypeMaps = true;

        // Product Title
        cfg.CreateMap<ProductTitle, ProductSummaryView>();
        cfg.CreateMap<ProductTitle, ProductView>();
        cfg.CreateMap<Product, ProductSummaryView>();
        cfg.CreateMap<Product, ProductSizeOption>();

        // Category
        cfg.CreateMap<Category, CategoryView>();

        // IProductAttribute
        cfg.CreateMap<IProductAttribute, Refinement>();

        // Basket
        cfg.CreateMap<DeliveryOption, DeliveryOptionView>();
        cfg.CreateMap<BasketItem, BasketItemView>();
        cfg.CreateMap<Basket, BasketView>();

        // Customer
        cfg.CreateMap<Customer, CustomerView>();
        cfg.CreateMap<DeliveryAddress, DeliveryAddressView>();

        // Orders
        cfg.CreateMap<Order, OrderView>();
        cfg.CreateMap<OrderItem, OrderItemView>();
        cfg.CreateMap<Address, DeliveryAddressView>();
        cfg.CreateMap<Order, OrderSummaryView>()
            .ForMember(dest => dest.IsSubmitted,
                    opt => opt.MapFrom(s => s.Status == OrderStatus.Submitted ? true : false));       
      });
    }
  }
  public class MoneyFormatter {
    public string Convert(decimal source) {
      return source.FormatMoney();
    }
  }
}
