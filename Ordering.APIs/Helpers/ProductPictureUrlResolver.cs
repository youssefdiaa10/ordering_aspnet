﻿using AutoMapper;
using Ordering.APIs.DTOs;
using Ordering.Core.Models;

namespace Ordering.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {

        private readonly IConfiguration _configuration;


        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}/{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
