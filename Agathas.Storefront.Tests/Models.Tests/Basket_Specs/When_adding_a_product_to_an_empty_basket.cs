using System;
using System.Linq;

using Agathas.Storefront.Models.Categories;
using Agathas.Storefront.Models.Products;

using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Models.Tests.Basket_Specs {
  [TestFixture]
  public class When_adding_a_product_to_an_empty_basket {
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
    }

    [Test]
    public void Then_the_basket_total_should_be_equal_to_the_cost_of_the_product() {
      _basket.BasketTotal.Should().Be(_product.Price);
    }

    [Test]
    public void Then_the_basket_should_contain_a_total_of_one_item() {
      _basket.NumberOfItemsInBasket().Should().Be(1);
    }

    [Test]
    public void Then_the_basket_should_contain_a_total_of_one_of_the_product() {
      _basket.Items.FirstOrDefault(i => i.Product == _product).Qty.Should().Be(1);
    }
  }
}
