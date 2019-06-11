using System;
using System.Linq;

using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Models.Customers;

namespace Agathas.Storefront.Models.Tests.Customer_Specs {
  [TestFixture]
  public class When_updating_a_customers_first_name_with_a_null_value : with_a_valid_customer {        
    public override void When()  {
      sut.FirstName = null;
    }
    
    [Test]
    public void Then_it_should_have_a_broken_rule_as_first_name_is_required_for_customer() {
      sut.GetBrokenRules().First(x => true).Property
        .Should().Be(CustomerBusinessRules.FirstNameRequired.Property);
    }
  }
}