using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;
using Microsoft.AspNetCore.Http;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Models.Basket;
using Agathas.Storefront.Models.Products;
using Agathas.Storefront.Models.Shipping;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Mapping;
using Agathas.Storefront.Services.Messaging.ProductCatalogService;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Implementations {
  public class BasketService : IBasketService {
    private readonly IBasketRepository _basketRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDeliveryOptionRepository _deliveryOptionRepository;
    private readonly IUnitOfWork<IHttpContextAccessor> _uow;
    private readonly IMapper _mapper;

    public BasketService(IBasketRepository basketRepository,
            IProductRepository productRepository,
            IDeliveryOptionRepository deliveryOptionRepository,
            IUnitOfWork<IHttpContextAccessor> uow,
            IMapper mapper) {
      _basketRepository = basketRepository;
      _productRepository = productRepository;
      _deliveryOptionRepository = deliveryOptionRepository;
      _uow = uow;
      _mapper = mapper;
    }

    public GetBasketResponse GetBasket(GetBasketRequest request) {
      var response = new GetBasketResponse();
      var basket = _basketRepository.FindBy(request.BasketId);
      BasketView basketView;

      if (basket != null) basketView = basket.ConvertToBasketView(_mapper);
      else basketView = new BasketView();

      response.Basket = basketView;
      return response;
    }

    public CreateBasketResponse CreateBasket(CreateBasketRequest request) {
      var response = new CreateBasketResponse();
      var basket = new Basket();

      basket.SetDeliveryOption(GetCheapestDeliveryOption());
      AddProductsToBasket(request.ProductsToAdd, basket);
      ThrowExceptionIfBasketIsInvalid(basket);

      _basketRepository.Save(basket);
      _uow.Commit();

      response.Basket = basket.ConvertToBasketView(_mapper);
      return response;
    }

    private DeliveryOption GetCheapestDeliveryOption() {
      return _deliveryOptionRepository.FindAll().OrderBy(d => d.Cost).FirstOrDefault();
    }

    private void ThrowExceptionIfBasketIsInvalid(Basket basket) {
      if (basket.GetBrokenRules().Count() > 0) {
        var brokenRules = new StringBuilder();
        brokenRules.AppendLine("There were problems saving the Basket:");

        foreach (BusinessRule businessRule in basket.GetBrokenRules()) {
          brokenRules.AppendLine(businessRule.Rule);
        }

        throw new ApplicationException(brokenRules.ToString());
      }
    }

    public ModifyBasketResponse ModifyBasket(ModifyBasketRequest request) {
      ModifyBasketResponse response = new ModifyBasketResponse();
      Basket basket = _basketRepository.FindBy(request.BasketId);

      if (basket == null) throw new BasketDoesNotExistException();

      AddProductsToBasket(request.ProductsToAdd, basket);
      UpdateLineQtys(request.ItemsToUpdate, basket);
      RemoveItemsFromBasket(request.ItemsToRemove, basket);

      if (request.SetShippingServiceIdTo != 0) {
        DeliveryOption deliveryOption =
                _deliveryOptionRepository.FindBy(request.SetShippingServiceIdTo);
        basket.SetDeliveryOption(deliveryOption);
      }

      ThrowExceptionIfBasketIsInvalid(basket);

      _basketRepository.Save(basket);
      _uow.Commit();

      response.Basket = basket.ConvertToBasketView(_mapper);
      return response;
    }

    private void RemoveItemsFromBasket(IList<int> productsToRemove, Basket basket) {
      foreach (int productId in productsToRemove) {
        Product product = _productRepository.FindBy(productId);
        if (product != null) basket.Remove(product);
      }
    }

    private void UpdateLineQtys(
            IList<ProductQtyUpdateRequest> productQtyUpdateRequests, Basket basket) {
      foreach (var productQtyUpdateRequest in productQtyUpdateRequests) {
        var product = _productRepository.FindBy(productQtyUpdateRequest.ProductId);

        if (product != null)
          basket.ChangeQtyOfProduct(productQtyUpdateRequest.NewQty, product);
      }
    }

    private void AddProductsToBasket(IList<int> productsToAdd, Basket basket) {
      Product product;

      if (productsToAdd.Count() > 0) {
        foreach (int productId in productsToAdd) {
          product = _productRepository.FindBy(productId);
          basket.Add(product);
        }
      }
    }

    public GetAllDispatchOptionsResponse GetAllDispatchOptions() {
      var response = new GetAllDispatchOptionsResponse();
      response.DeliveryOptions = _deliveryOptionRepository.FindAll()
              .OrderBy(d => d.Cost).ConvertToDeliveryOptionViews(_mapper);

      return response;
    }
  }
}
