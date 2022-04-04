﻿using AutoMapper;
using Insurance.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Profiles
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<ProductDto, InsuranceDto>()
                    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.SalesPrice, opt => opt.MapFrom(src => src.SalesPrice));

            CreateMap<ProductTypeDto, InsuranceDto>()
                   .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.Name))
                   .ForMember(dest => dest.ProductTypeHasInsurance, opt => opt.MapFrom(src => src.CanBeInsured));

        }

    }
}
