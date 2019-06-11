using System;

namespace Agathas.Storefront.Models.Customers {
  public class InvalidAddressException : Exception {
    public InvalidAddressException(string message) : base(message) { }
  }
}
