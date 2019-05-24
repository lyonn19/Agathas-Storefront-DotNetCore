using System.Linq;

using Agathas.Storefront.Model.Basket;
using Agathas.Storefront.Model.Products;

using NUnit.Framework;
using FluentAssertions;

namespace Agathas.Storefront.Model.Tests.Basket_Item_Specs {
  [TestFixture]
  public class When_a_basket_item_is_created_with_a_null_basket {
    private BasketItem _basketItem;

    [SetUp]
    public void Given() {
      _basketItem = new BasketItem(new Product(), null, 1);
    }

    [Test]
    public void Then_it_should_have_1_broken_rule() {
      _basketItem.GetBrokenRules().Count().Should().Be(1);           
    }

    [Test]
    public void Then_it_should_have_a_broken_rule_highlighting_the_requirement_for_a_basket() {    
      _basketItem.GetBrokenRules().First(x => true).Rule
        .Should().Be(BasketItemBusinessRules.BasketRequired.Rule);

      _basketItem.GetBrokenRules().First(x => true).Property
        .Should().Be(BasketItemBusinessRules.BasketRequired.Property);
    }
  }
}