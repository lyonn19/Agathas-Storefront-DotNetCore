using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Models.Tests.Basket_Specs {
  [TestFixture]
  public class When_not_selecting_a_delivery_option_for_a_basket {
    private Basket.Basket _basket;        

    [SetUp]
    public void Given() {
      _basket = new Basket.Basket();            
    }

    [Test]
    public void Then_the_delivery_cost_should_be_0() {
      _basket.DeliveryCost().Should().Be(0);
    }
  }
}
