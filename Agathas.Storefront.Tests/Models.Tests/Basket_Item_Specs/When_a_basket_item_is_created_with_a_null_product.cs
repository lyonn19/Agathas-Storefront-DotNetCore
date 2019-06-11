using System;
using System.Linq;

using Agathas.Storefront.Models.Basket;

using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Models.Tests.Basket_Item_Specs {
  [TestFixture]
  public class When_a_basket_item_is_created_with_a_null_product {
    private BasketItem _basketItem;

    [SetUp]
    public void Given() {
      _basketItem = new BasketItem(null, new Basket.Basket(), 1);
    }

    [Test]
    public void Then_it_should_have_a_broken_rule_highlighting_the_requirement_for_a_product() {   
      _basketItem.GetBrokenRules().First(x => true).Rule
        .Should().Be(BasketItemBusinessRules.ProductRequired.Rule);

      _basketItem.GetBrokenRules().First(x => true).Property
        .Should().Be(BasketItemBusinessRules.ProductRequired.Property);
    }

    [Test]
    public void Then_it_should_have_1_broken_rule() {
      _basketItem.GetBrokenRules().Count().Should().Be(1);
    }
  }
}
