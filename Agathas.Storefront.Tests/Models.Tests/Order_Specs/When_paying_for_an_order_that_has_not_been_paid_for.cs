using System;
using System.Collections.Generic;

using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Model.Categories;
using Agathas.Storefront.Model.Orders;
using Agathas.Storefront.Model.Orders.States;
using Agathas.Storefront.Model.Products;

namespace Agathas.Storefront.Model.Tests.Order_Specs {    	
  [TestFixture]
  public class When_paying_for_an_order_that_has_not_been_paid_for {
    private Order _order;

    [SetUp]
    public void Given() {
      _order = new Order();

      ProductTitle productTitle = new ProductTitle();
      productTitle.Name = "Hat";
      productTitle.Price = 9.00m;
      productTitle.Brand = new Brand();
      productTitle.Category = new Category();
      productTitle.Color = new ProductColor();
      productTitle.Products = new List<Product>();

      Product product = new Product();
      product.Title = productTitle;
      product.Size = new ProductSize();

      _order.AddItem(product, 1);

      Payment payment = new Payment(DateTime.Now, "fffljhkjkj", "PayPal", product.Price);

      _order.SetPayment(payment);
    }

    [Test]
    public void Then_the_order_should_be_in_a_submitted_state() { 
      _order.Status.Should().Be(OrderStates.Submitted.Status); 
    }
  }
}
