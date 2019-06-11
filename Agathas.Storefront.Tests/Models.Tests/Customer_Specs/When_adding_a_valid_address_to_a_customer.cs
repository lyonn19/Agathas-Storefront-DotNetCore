using System;
using System.Linq;

using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Models.Customers;

namespace Agathas.Storefront.Models.Tests.Customer_Specs {
  [TestFixture]
  public class When_adding_a_valid_address_to_a_customer : with_a_valid_customer  {
    private DeliveryAddress _address;

    public override void When()  {
      _address = new DeliveryAddress();
      _address.Customer = base.sut;
      _address.Name = "My Work Pad";
      _address.AddressLine1 = "123 Not Exist street";
      _address.AddressLine2 = String.Empty;
      _address.City = "Carlisle";
      _address.State = "South Carolina";
      _address.Country = "United States";
      _address.ZipCode = "29031";

      base.sut.AddAddress(_address);
    }

    [Test]
    public void Then_the_address_should_appear_in_the_customers_list() {
      base.sut.DeliveryAddressBook.Count().Should().Be(1);
      base.sut.DeliveryAddressBook.Any(a => a == _address).Should().BeTrue();
    }
  }
}
