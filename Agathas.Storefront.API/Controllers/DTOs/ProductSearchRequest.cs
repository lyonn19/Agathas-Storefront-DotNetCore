﻿using System.Collections.Generic;

using Agathas.Storefront.Services.Messaging.ProductCatalogService;

namespace Agathas.Storefront.API.Controllers.DTOs {
  public class ProductSearchRequest {    
    public int CategoryId { get; set; }    
    public int[] ColorIds { get; set; }    
    public int[] SizeIds { get; set; }    
    public int[] BrandIds { get; set; }    
    public ProductsSortBy SortBy { get; set; }    
    public IEnumerable<RefinementGroup> RefinementGroups { get; set; }    
    public int Index { get; set; }
  }    
}
