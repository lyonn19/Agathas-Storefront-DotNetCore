using Agathas.Storefront.Models.Orders;
using NUnit.Framework;

namespace Agathas.Storefront.Models.Tests.Order_Specs {
  [TestFixture]
  public abstract class with_valid_order {
    [SetUp]
    public void SetUp() {
      sut = new Order();
      When();
    }

    public Order sut { get; set; }

    public abstract void When();
  }
}