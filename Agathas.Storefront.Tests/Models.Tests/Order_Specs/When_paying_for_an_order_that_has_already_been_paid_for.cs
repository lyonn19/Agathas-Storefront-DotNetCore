using System;
using System.Collections.Generic;

using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Models.Categories;
using Agathas.Storefront.Models.Orders;
using Agathas.Storefront.Models.Orders.States;
using Agathas.Storefront.Models.Products;

namespace Agathas.Storefront.Models.Tests.Order_Specs {
  [TestFixture]
  public class When_paying_for_an_order_that_has_already_been_paid_for : Order {
    [Test]
    public void Then_an_OrderAlreadyPaidForException_should_be_thrown() {
      var productTitle = new ProductTitle();
      productTitle.Name = "Hat";
      productTitle.Price = 9.00m;
      productTitle.Brand = new Brand();
      productTitle.Category = new Category();
      productTitle.Color = new ProductColor();
      productTitle.Products = new List<Product>();

      Product product = new Product();
      product.Title = productTitle;
      product.Size = new ProductSize();

      this.AddItem(product, 1);

      var payment = new Payment(DateTime.Now, "fffljhkjkj", "PayPal", product.Price);
      this.SetPayment(payment);

      Action act = () => this.SetPayment(payment);
      act.Should().Throw<OrderAlreadyPaidForException>();
    }
  }
}
