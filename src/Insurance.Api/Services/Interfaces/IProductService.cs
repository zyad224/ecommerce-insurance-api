﻿using Insurance.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ProductTypeDto> GetProductType(ProductDto productDto);
        public Task<ProductDto> GetProduct(int productID);

    }
}
