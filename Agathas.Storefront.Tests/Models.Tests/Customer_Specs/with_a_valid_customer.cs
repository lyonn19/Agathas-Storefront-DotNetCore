using Agathas.Storefront.Model.Customers;
using NUnit.Framework;

namespace Agathas.Storefront.Model.Tests.Customer_Specs {
  [TestFixture]
  public abstract class with_a_valid_customer {
    [SetUp]
    public void Context() {
      sut = new Customer();
      sut.IdentityToken = "jhkjhkjhkj";
      sut.FirstName = "Scott";
      sut.SecondName = "Millett";
      sut.Email = "Scott@elbandit.co.uk";

      When();
    }

    public abstract void When();        

    public Customer sut { get; set; }
  }
}