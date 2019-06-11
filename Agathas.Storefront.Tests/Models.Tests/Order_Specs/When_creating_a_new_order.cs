using System;
using System.Collections.Generic;

using Agathas.Storefront.Infrastructure.Domain.Events;
using Agathas.Storefront.Models.Categories;
using Agathas.Storefront.Models.Orders;
using Agathas.Storefront.Models.Orders.States;
using Agathas.Storefront.Models.Products;

using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Models.Tests.Order_Specs {    
  [TestFixture]
  public class When_creating_a_new_order {
    private Order _order;

    [SetUp]
    public void Given() {
      DomainEvents.DomainEventHandlerFactory = new StubDomainEventHandlerFactory();

      _order = new Order();

      var productTitle = new ProductTitle();
      productTitle.Name = "Hat";
      productTitle.Price = 9.00m;
      productTitle.Brand = new Brand();
      productTitle.Category = new Category();
      productTitle.Color = new ProductColor();
      productTitle.Products = new List<Product>();

      var product = new Product();
      product.Title = productTitle;
      product.Size = new ProductSize();

      _order.AddItem(product, 1);
    }

    [Test]
    public void Then_the_order_should_be_in_an_open_state() {
      _order.Status.Should().Be(OrderStates.Open.Status);
    }
  }
}
