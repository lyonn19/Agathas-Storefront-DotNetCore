using System;
using System.Linq;

using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Models.Customers;

namespace Agathas.Storefront.Models.Tests.Address_Specs {
  [TestFixture]
  public class When_creating_a_new_address_with_a_blank_state : Address {
    [Test]
    public void Then_it_should_have_a_broken_rule_as_state_is_required() {
      this.AddressLine1 = "123 Not Exist street";
      this.AddressLine2 = String.Empty;
      this.City = "Carlisle";
      this.State = String.Empty;
      this.Country = "United States";
      this.ZipCode = "29031";

      this.GetBrokenRules().First(x => true).Rule
        .Should().Be(AddressBusinessRules.StateRequired.Rule);
    }
  }
}