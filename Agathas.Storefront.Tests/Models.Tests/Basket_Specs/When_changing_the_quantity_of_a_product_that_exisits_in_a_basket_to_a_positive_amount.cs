using System;
using System.Linq;

using Agathas.Storefront.Models.Categories;
using Agathas.Storefront.Models.Products;

using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Models.Tests.Basket_Specs {
  [TestFixture]
  public class When_changing_the_quantity_of_a_product_that_exisits_in_a_basket_to_a_positive_amount {
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
    public void Then_the_quantity_of_that_product_should_update_to_match() {
      var newQty = 5;

      _basket.ChangeQtyOfProduct(newQty, _product);

      _basket.Items.FirstOrDefault(i => i.Product == _product).Qty
        .Should().Be(newQty);
    }        
  }
}
