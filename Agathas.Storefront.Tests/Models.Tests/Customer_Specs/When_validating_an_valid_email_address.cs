using NUnit.Framework;
using FluentAssertions;

using Agathas.Storefront.Model.Customers;

namespace Agathas.Storefront.Model.Tests.Customer_Specs {
  [TestFixture()]
  public class When_validating_an_valid_email_address {
    private string _validEmailAddress;
    private EmailValidationSpecification _emailValidationSpecification;

    [SetUp]
    public void Given() {
      _validEmailAddress = "scott@elbandit.co.uk";
      _emailValidationSpecification = new EmailValidationSpecification();
    }
    
    [Test]
    public void A_Valid_Email_Address_Will_Satisfiy_The_Email_Validation_Specification() {
      _emailValidationSpecification.IsSatisfiedBy(_validEmailAddress)
        .Should().BeTrue();
    }
  }
}