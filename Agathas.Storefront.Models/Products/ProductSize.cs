﻿using System;
using Agathas.Storefront.Infrastructure.Domain;

namespace Agathas.Storefront.Models.Products
{
    public class ProductSize : EntityBase<int>, IProductAttribute
    {        
        public string Name { get; set; }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
