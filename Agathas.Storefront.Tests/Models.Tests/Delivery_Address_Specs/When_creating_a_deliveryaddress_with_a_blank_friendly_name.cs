using System;
using System.Linq;

using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Models.Customers;

namespace Agathas.Storefront.Models.Tests.Delivery_Address_Specs {
  [TestFixture]
  public class When_creating_a_deliveryaddress_with_a_blank_friendly_name : DeliveryAddress {
    [Test]
    public void Then_it_should_have_a_broken_rule_as_name_should_be_valid_for_delivery_address() {
      this.Name = string.Empty;
      this.AddressLine1 = "123 Not Exist Rd.";
      this.AddressLine2 = String.Empty;
      this.City = "Carlisle";
      this.State = "SC";
      this.Country = "United States";
      this.ZipCode = "29031";this.GetBrokenRules().First(x => true).Property
        .Should().Be(DeliveryAddressBusinessRules.NameRequired.Property);
    }
  }
}