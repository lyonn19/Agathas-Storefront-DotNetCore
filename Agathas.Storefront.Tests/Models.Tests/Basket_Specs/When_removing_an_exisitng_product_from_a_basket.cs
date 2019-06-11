using System;
using System.Linq;

using Agathas.Storefront.Models.Categories;
using Agathas.Storefront.Models.Products;

using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Models.Tests.Basket_Specs {
  [TestFixture]
  public class When_removing_an_exisitng_product_from_a_basket {
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
    public void Then_the_product_should_no_longer_be_in_the_basket() {
      _basket.Remove(_product);

      _basket.Items.FirstOrDefault(i => i.Product == _product).Should().BeNull();
      _basket.NumberOfItemsInBasket().Should().Be(0);
    }
  }
}
