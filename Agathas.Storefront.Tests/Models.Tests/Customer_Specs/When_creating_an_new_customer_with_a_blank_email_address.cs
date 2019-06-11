using System;
using System.Linq;

using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Models.Customers;

namespace Agathas.Storefront.Models.Tests.Customer_Specs {
  [TestFixture]
  public class When_creating_an_new_customer_with_a_blank_email_address : Customer {
    [Test]
    public void Then_it_should_have_a_broken_rule_as_email_is_required_for_customer() {   
      this.IdentityToken = "jhkjhkjhkj";
      this.FirstName = "Scott";
      this.SecondName = "Millett";
      this.Email = String.Empty;

      DeliveryAddress address = new DeliveryAddress();
      address.Customer = this;
      address.Name = "My Work Pad";
      address.AddressLine1 = "123 Not Exist street";
      address.AddressLine2 = String.Empty;
      address.City = "Carlisle";
      address.State = "South Carolina";
      address.Country = "United States";
      address.ZipCode = "29031";

      this.AddAddress(address);
      this.GetBrokenRules().First(x => true).Property
        .Should().Be(CustomerBusinessRules.EmailRequired.Property);
    }
  }
}
