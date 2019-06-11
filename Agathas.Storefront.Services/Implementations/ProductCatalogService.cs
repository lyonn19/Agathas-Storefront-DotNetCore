using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using Agathas.Storefront.Infrastructure.Domain;
using Agathas.Storefront.Infrastructure.Querying;
using Agathas.Storefront.Infrastructure.Helpers;
using Agathas.Storefront.Models.Categories;
using Agathas.Storefront.Models.Products;
using Agathas.Storefront.Services.Interfaces;
using Agathas.Storefront.Services.Mapping;
using Agathas.Storefront.Services.Messaging.ProductCatalogService;

namespace Agathas.Storefront.Services.Implementations {
  public class ProductCatalogService : IProductCatalogService {
    private readonly IProductTitleRepository _productTitleRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public ProductCatalogService(IProductTitleRepository productTitleRepository,
                                    IProductRepository productRepository,
                                    ICategoryRepository categoryRepository,
                                    IMapper mapper) {
      _productTitleRepository = productTitleRepository;
      _productRepository = productRepository;
      _categoryRepository = categoryRepository;
      _mapper = mapper;
    }

    private IEnumerable<Product> GetAllProductsMatchingQueryAndSort(
            GetProductsByCategoryRequest request, Query productQuery) {
      var productsMatchingRefinement = _productRepository.FindBy(productQuery);

      switch (request.SortBy) {
        case ProductsSortBy.PriceLowToHigh:
          productsMatchingRefinement = productsMatchingRefinement.OrderBy(p => p.Price);
          break;
        case ProductsSortBy.PriceHighToLow:
          productsMatchingRefinement = productsMatchingRefinement
                  .OrderByDescending(p => p.Price);
          break;
      }
      return productsMatchingRefinement;
    }

    public GetFeaturedProductsResponse GetFeaturedProducts() {
      var response = new GetFeaturedProductsResponse();
      var productQuery = new Query();

      productQuery.OrderByProperty = new OrderByClause() { 
              Desc = true, PropertyName = 
                  PropertyNameHelper.ResolvePropertyName<ProductTitle>(pt => pt.Price) };

      response.Products = _productTitleRepository.FindBy(
              productQuery, 0, 6).ConvertToProductViews(_mapper);

      return response;
    }

    public GetProductsByCategoryResponse GetProductsByCategory(
            GetProductsByCategoryRequest request) {
      GetProductsByCategoryResponse response;

      var productQuery = ProductSearchRequestQueryGenerator.CreateQueryFor(request);
      var productsMatchingRefinement = 
              GetAllProductsMatchingQueryAndSort(request, productQuery);

      response = productsMatchingRefinement.CreateProductSearchResultFrom(request, _mapper);
      response.SelectedCategoryName =
              _categoryRepository.FindBy(request.CategoryId).Name;
      return response;
    }

    public GetProductResponse GetProduct(GetProductRequest request) {
      var response = new GetProductResponse();
      var productTitle = _productTitleRepository.FindBy(request.ProductId);

      response.Product = productTitle.ConvertToProductDetailView(_mapper);
      return response;
    }

    public GetAllCategoriesResponse GetAllCategories() {
      var response = new GetAllCategoriesResponse();
      var categories = _categoryRepository.FindAll();
      response.Categories = categories.ConvertToCategoryViews(_mapper);

      return response;
    }
  }
}
