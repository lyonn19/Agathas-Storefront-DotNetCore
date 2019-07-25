using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;
using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Customers;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Mapping;
using Agathas.Storefront.Services.Messaging.CustomerService;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Implementations {
  public class CustomerService : ICustomerService {
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork<IHttpContextAccessor> _uow;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository,
                            IUnitOfWork<IHttpContextAccessor> uow, IMapper mapper) {
      _customerRepository = customerRepository;
      _uow = uow;
      _mapper = mapper;
    }

    public CreateCustomerResponse CreateCustomer(CreateCustomerRequest request) {
      var response = new CreateCustomerResponse();
      var customer = new Customer();

      customer.IdentityToken = request.CustomerIdentityToken;
      customer.Email = request.Email;
      customer.FirstName = request.FirstName;
      customer.SecondName = request.SecondName;

      ThrowExceptionIfCustomerIsInvalid(customer);

      _customerRepository.Add(customer);
      _uow.Commit();

      response.Customer = customer.ConvertToCustomerDetailView(_mapper);
      return response;
    }

    private void ThrowExceptionIfCustomerIsInvalid(Customer customer) {
      if (customer.GetBrokenRules().Count() > 0) {
        var brokenRules = new StringBuilder();
        brokenRules.AppendLine("There were problems saving the Customer:");
        foreach (BusinessRule businessRule in customer.GetBrokenRules()) {
          brokenRules.AppendLine(businessRule.Rule);
        }

        throw new CustomerInvalidException(brokenRules.ToString());
      }
    }

    public GetCustomerResponse GetCustomer(GetCustomerRequest request) {
      var response = new GetCustomerResponse();
      var customer = _customerRepository.FindBy(request.CustomerIdentityToken);

      if (customer != null) {
        response.CustomerFound = true;
        response.Customer = customer.ConvertToCustomerDetailView(_mapper);
        if (request.LoadOrderSummary)
          response.Orders = customer.Orders.OrderByDescending(
                    o => o.Created).ConvertToOrderSummaryViews(_mapper);
      } else response.CustomerFound = false;

      return response;
    }

    public ModifyCustomerResponse ModifyCustomer(ModifyCustomerRequest request) {
      var response = new ModifyCustomerResponse();
      var customer = _customerRepository.FindBy(request.CustomerIdentityToken);

      customer.FirstName = request.FirstName;
      customer.SecondName = request.SecondName;
      customer.Email = request.Email;

      ThrowExceptionIfCustomerIsInvalid(customer);

      _customerRepository.Save(customer);
      _uow.Commit();

      response.Customer = customer.ConvertToCustomerDetailView(_mapper);
      return response;
    }

    public DeliveryAddressModifyResponse ModifyDeliveryAddress(
                                              DeliveryAddressModifyRequest request) {
      var response = new DeliveryAddressModifyResponse();
      var customer = _customerRepository.FindBy(request.CustomerIdentityToken);

      var deliveryAddress = customer.DeliveryAddressBook
              .Where(d => d.Id == request.Address.Id)
              .FirstOrDefault();

      if (deliveryAddress != null) {
        UpdateDeliveryAddressFrom(request.Address, deliveryAddress);

        _customerRepository.Save(customer);
        _uow.Commit();
      }

      response.DeliveryAddress = deliveryAddress.ConvertToDeliveryAddressView(_mapper);
      return response;
    }

    public DeliveryAddressAddResponse AddDeliveryAddress( DeliveryAddressAddRequest request) {
      var response = new DeliveryAddressAddResponse();
      var customer = _customerRepository.FindBy(request.CustomerIdentityToken);
      var deliveryAddress = new DeliveryAddress();

      deliveryAddress.Customer = customer;
      UpdateDeliveryAddressFrom(request.Address, deliveryAddress);

      customer.AddAddress(deliveryAddress);

      _customerRepository.Save(customer);
      _uow.Commit();

      response.DeliveryAddress = deliveryAddress.ConvertToDeliveryAddressView(_mapper);
      return response;
    }

    private void UpdateDeliveryAddressFrom(
                                  DeliveryAddressView deliveryAddressSource,
                                            DeliveryAddress deliveryAddressToUpdate) {
      deliveryAddressToUpdate.Name = deliveryAddressSource.Name;
      deliveryAddressToUpdate.AddressLine1 =
                                      deliveryAddressSource.AddressLine1;
      deliveryAddressToUpdate.AddressLine2 =
                                      deliveryAddressSource.AddressLine2;
      deliveryAddressToUpdate.City = deliveryAddressSource.City;
      deliveryAddressToUpdate.State = deliveryAddressSource.State;
      deliveryAddressToUpdate.Country = deliveryAddressSource.Country;
      deliveryAddressToUpdate.ZipCode = deliveryAddressSource.ZipCode;
    }
  }
}
