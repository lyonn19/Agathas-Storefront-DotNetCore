using System;

using NUnit.Framework;

using Agathas.Storefront.Models.Customers;

namespace Agathas.Storefront.Models.Tests.Delivery_Address_Specs {
  [TestFixture]
  public abstract class with_a_valid_delivery_address {
    [SetUp]
    public void SetUp() {
      var address = new Address();
      address.AddressLine1 = "123 Not Exist street";
      address.AddressLine2 = String.Empty;
      address.City = "Carlisle";
      address.State = "South Carolina";
      address.Country = "United States";
      address.ZipCode = "29031";

      var customer = new Customer();
      customer.IdentityToken = "jhkjhkjhkj";
      customer.FirstName = "Scott";
      customer.SecondName = "Millett";
      customer.Email = "Scott@elbandit.co.uk";

      sut = new DeliveryAddress();
      sut.Customer = customer;
      sut.Name = "My Work Pad";
      sut.AddressLine1 = address.AddressLine1;
      sut.AddressLine2 = address.AddressLine2;
      sut.City = address.City;
      sut.State = address.State;
      sut.Country = address.Country;
      sut.ZipCode = address.ZipCode;

      When();
    }

    public DeliveryAddress sut { get; set; }

    public abstract void When();
  }
}