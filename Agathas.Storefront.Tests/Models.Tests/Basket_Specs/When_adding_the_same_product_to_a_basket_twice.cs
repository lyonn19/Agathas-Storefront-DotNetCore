using System;
using System.Linq;

using Agathas.Storefront.Model.Categories;
using Agathas.Storefront.Model.Products;

using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Model.Tests.Basket_Specs {
  [TestFixture]
  public class When_adding_the_same_product_to_a_basket_twice {
    private Basket.Basket _basket;
    private Product _product;

    [SetUp]
    public void Given() {
      _basket = new Basket.Basket();

      var productTitle = new ProductTitle();
      productTitle.Name = "Product A";
      productTitle.Price = 15.00m;
      productTitle.Brand = new Brand();
      productTitle.Category = new Category();
      productTitle.Color = new ProductColor();
      productTitle.Products = null;

      _product = new Product();
      _product.Title = productTitle;
      _product.Size = new ProductSize();

      _basket.Add(_product);
      _basket.Add(_product);
    }

    [Test]
    public void Then_the_basket_total_should_be_equal_to_the_cost_of_2x_the_product() {
      _basket.BasketTotal.Should().Be(_product.Price * 2);
    }

    [Test]
    public void Then_the_total_number_of_items_in_a_basket_should_be_equal_to_2() {
      _basket.NumberOfItemsInBasket().Should().Be(2);
    }

    [Test]
    public void Then_the_basket_items_total_should_be_equal_to_2x_the_cost_of_the_product() {
      _basket.ItemsTotal.Should().Be(_product.Price * 2);
    }

    [Test]
    public void Then_the_quantity_for_the_product_should_be_2() {
      _basket.Items().FirstOrDefault(i => i.Product == _product).Qty.Should().Be(2);
    }

    [Test]
    public void Then_the_line_total_for_the_product_should_equal_to_the_cost_of_2x_the_product() {
      _basket.Items().FirstOrDefault(i => i.Product == _product).LineTotal()
        .Should().Be(_product.Price * 2);
    }       
  }
}
