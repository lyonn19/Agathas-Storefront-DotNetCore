﻿using System;
using System.Linq;

using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Model.Customers;

namespace Agathas.Storefront.Model.Tests.Address_Specs {
  [TestFixture]
  public class When_creating_a_new_address_with_a_blank_country : Address {
    [Test]
    public void Then_it_should_have_a_broken_rule_as_country_is_required_for_address() {
      this.AddressLine1 = "123 Not Exist street";
      this.AddressLine2 = String.Empty;
      this.City = "Carlisle";
      this.State = "South Carolina";
      this.Country = String.Empty;
      this.ZipCode = "29031";

      this.GetBrokenRules().First(x => true).Rule
        .Should().Be(AddressBusinessRules.CountryRequired.Rule);
    }
  }
}