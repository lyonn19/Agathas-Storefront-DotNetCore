using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agathas.Storefront.Infrastructure.Payments;
using Agathas.Storefront.Services.ViewModels;
using AutoMapper;

namespace Agathas.Storefront.API.Controllers.DTOs {
  public static class OrderMapper {
    public static OrderPaymentRequest ConvertToOrderPaymentRequest(OrderView order) {
      return Mapper.Map<OrderView, OrderPaymentRequest>(order);
    }
  }
}
