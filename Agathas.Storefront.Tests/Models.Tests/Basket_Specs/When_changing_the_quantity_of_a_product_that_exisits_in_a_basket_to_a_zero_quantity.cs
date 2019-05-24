using System.Linq;

using Agathas.Storefront.Model.Categories;
using Agathas.Storefront.Model.Products;

using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Model.Tests.Basket_Specs {
  [TestFixture]
  public class When_changing_the_quantity_of_a_product_that_exisits_in_a_basket_to_a_zero_quantity {
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
    public void Then_the_product_should_be_removed() {
      var newQty = 0;

      _basket.ChangeQtyOfProduct(newQty, _product);

      _basket.Items().FirstOrDefault(i => i.Product == _product).Qty.Should().Be(0);
    }
  }
}