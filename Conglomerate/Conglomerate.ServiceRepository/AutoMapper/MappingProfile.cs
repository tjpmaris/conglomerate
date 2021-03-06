﻿using AutoMapper;

using Conglomerate.Data.Entities.SandwichShop;
using Conglomerate.ServiceRepository.Models;

namespace Conglomerate.ServiceRepository.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ingredient, IngredientLogic>();
            CreateMap<IngredientLogic, Ingredient>();
        }
    }
}
