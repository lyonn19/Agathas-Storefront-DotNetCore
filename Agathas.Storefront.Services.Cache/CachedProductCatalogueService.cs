﻿using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Agathas.Storefront.Models.Products;
using Agathas.Storefront.Services.Cache.CacheStorage;
using Agathas.Storefront.Services.Cache.Specifications;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Mapping;
using Agathas.Storefront.Services.Messaging.ProductCatalogService;
using Agathas.Storefront.Services.ViewModels;

namespace Agathas.Storefront.Services.Cache {
  public class CachedProductCatalogService : IProductCatalogService {
    private readonly ICacheStorage _cachStorage;
    private readonly IProductCatalogService _realProductCatalogService;
    private readonly IProductTitleRepository _productTitleRepository;
    private readonly IProductRepository _productRepository;
    private readonly Object _getTopSellingProductsLock = new Object();
    private readonly Object _getAllProductTitlesLock = new Object();
    private readonly Object _getAllProductsLock = new Object();
    private readonly object _getAllCategoriesLock = new object();
    private readonly IMapper _mapper;

    public CachedProductCatalogService(ICacheStorage cachStorage,
                                          IProductCatalogService realProductCatalogService,
                                          IProductTitleRepository productTitleRepository,
                                          IProductRepository productRepository,
                                          IMapper mapper) {
      _cachStorage = cachStorage;
      _realProductCatalogService = realProductCatalogService;
      _productTitleRepository = productTitleRepository;
      _productRepository = productRepository;
      _mapper = mapper;
    }

    private IEnumerable<ProductTitle> FindAllProductTitles() {
      lock (_getAllProductTitlesLock) {
        IEnumerable<ProductTitle> allProductTitles;

        allProductTitles =
            _cachStorage.Retrieve<IEnumerable<ProductTitle>>(CacheKeys.AllProductTitles.ToString());

        if (allProductTitles == null) {
          allProductTitles = _productTitleRepository.FindAll();
          _cachStorage.Store(CacheKeys.AllProductTitles.ToString(), allProductTitles);
        }

        return allProductTitles;
      }
    }

    private IEnumerable<Product> FindAllProducts() {
      lock (_getAllProductsLock) {
        IEnumerable<Product> allProducts;

        allProducts = _cachStorage.Retrieve<IEnumerable<Product>>(CacheKeys.AllProducts.ToString());

        if (allProducts == null) {
          allProducts = _productRepository.FindAll();
          _cachStorage.Store(CacheKeys.AllProducts.ToString(), allProducts);
        }

        return allProducts;
      }
    }

    public GetFeaturedProductsResponse GetFeaturedProducts() {
      lock (_getTopSellingProductsLock) {
        var response = new GetFeaturedProductsResponse();
        IEnumerable<ProductSummaryView> productViews;

        productViews =
            _cachStorage.Retrieve<IEnumerable<ProductSummaryView>>(
                      CacheKeys.TopSellingProducts.ToString());

        if (productViews == null) {
          response = _realProductCatalogService.GetFeaturedProducts();
          _cachStorage.Store(CacheKeys.TopSellingProducts.ToString(), response.Products);
        } else response.Products = productViews; 

        return response;
      }
    }

    public GetProductsByCategoryResponse GetProductsByCategory(GetProductsByCategoryRequest request) {
      var colourSpecification = new ProductIsInColourSpecification(request.ColorIds);
      var brandSpecification = new ProductIsInBrandSpecification(request.BrandIds);
      var sizeSpecification = new ProductIsInSizeSpecification(request.SizeIds);
      var categorySpecification = new ProductIsInCategorySpecification(request.CategoryId);
      var matchingProducts = FindAllProducts().Where(colourSpecification.IsSatisfiedBy)
          .Where(brandSpecification.IsSatisfiedBy)
          .Where(sizeSpecification.IsSatisfiedBy)
          .Where(categorySpecification.IsSatisfiedBy);

      switch (request.SortBy) {
        case ProductsSortBy.PriceLowToHigh:
          matchingProducts = matchingProducts.OrderBy(p => p.Price);
          break;
        case ProductsSortBy.PriceHighToLow:
          matchingProducts = matchingProducts.OrderByDescending(p => p.Price);
          break;
      }

      var response = matchingProducts.CreateProductSearchResultFrom(request, _mapper);

      response.SelectedCategoryName =  GetAllCategories().Categories
              .Where(c => c.Id == request.CategoryId).FirstOrDefault().Name; 
      return response;
    }
    
    public GetProductResponse GetProduct(GetProductRequest request) {
      var response = new GetProductResponse();
      response.Product = FindAllProductTitles().Where(p => p.Id == request.ProductId)
              .FirstOrDefault().ConvertToProductDetailView(_mapper);
      return response;
    }

    public GetAllCategoriesResponse GetAllCategories() {
      lock (_getAllCategoriesLock) {
        var response = _cachStorage.Retrieve<GetAllCategoriesResponse>(
                  CacheKeys.AllCategories.ToString());

        if (response == null) {
          response = _realProductCatalogService.GetAllCategories();
          _cachStorage.Store(CacheKeys.AllCategories.ToString(), response);
        }

        return response;
      }
    }
  }
}
    