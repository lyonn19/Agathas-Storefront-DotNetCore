using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Models.Customers;

namespace Agathas.Storefront.Models.Tests.Delivery_Address_Specs {
  [TestFixture]
  public class When_changing_the_name_of_a_delivery_address_to_an_invalid_value : with_a_valid_delivery_address {
    
    public override void When()  {
      sut.Name = String.Empty;
    }

    [Test]
    public void Then_it_should_have_a_broken_rule_as_name_should_be_valid_for_delivery_address() {
      sut.GetBrokenRules().First(x => true).Property
        .Should().Be(DeliveryAddressBusinessRules.NameRequired.Property);
    }
  }
}
