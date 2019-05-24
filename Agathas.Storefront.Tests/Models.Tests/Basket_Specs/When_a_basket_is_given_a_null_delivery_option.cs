using System;
using System.Linq;

using Agathas.Storefront.Model.Basket;

using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Model.Tests.Basket_Specs {
  [TestFixture]
  public class When_a_basket_is_given_a_null_delivery_option {
    private Basket.Basket _basket;        

    [SetUp]
    public void Given() {
      _basket = new Basket.Basket();
      _basket.SetDeliveryOption(null);            
    }

    [Test]
    public void Then_the_basket_should_have_1_broken_rule() {
      _basket.GetBrokenRules().Count().Should().Be(1);
    }

    [Test]
    public void Then_the_basket_should_have_a_broken_rule_highlighting_the_invalid_delivery_option() {
      _basket.GetBrokenRules().First(x => true).Rule
        .Should().Be(BasketBusinessRules.DeliveryOptionRequired.Rule);
      _basket.GetBrokenRules().First(x => true).Property
        .Should().Be(BasketBusinessRules.DeliveryOptionRequired.Property);
    }
  }
}
