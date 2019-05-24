using System;

using Agathas.Storefront.Model.Shipping;

using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Model.Tests.Shipping_Specs {
  [TestFixture]
  public class When_determining_the_cost_of_delivery_with_a_basket_that_exceeds_the_free_delivery_threshold {
    private DeliveryOption _deliveryOption;
    private decimal _freeDeliveryThreshold;

    [SetUp]
    public void Given() {
      _freeDeliveryThreshold = 50m;        
      _deliveryOption = new DeliveryOption(_freeDeliveryThreshold, 10m, null);
    }

    [Test]
    public void Then_the_cost_of_delivery_should_be_0() {
      var value = _deliveryOption.GetDeliveryChargeForBasketTotalOf(_freeDeliveryThreshold * 2);
      value.Should().Be(0);
    }
  }
}
