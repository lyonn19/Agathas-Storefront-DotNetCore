using System;

using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Model.Customers;

namespace Agathas.Storefront.Model.Tests.Customer_Specs {
  [TestFixture()]
  public class When_validating_an_invalid_email_address {
    private string _invalidEmailAddress;
    private EmailValidationSpecification _emailValidationSpecification;

    [SetUp]
    public void Given() {
      _invalidEmailAddress = "gg@kkkkk";
      _emailValidationSpecification = new EmailValidationSpecification();
    }

    [Test]
    public void Then_the_email_address_will_not_satisfiy_the_email_validation_specification() {
      _emailValidationSpecification.IsSatisfiedBy(_invalidEmailAddress)
        .Should().BeFalse();
    }              
  }
}
