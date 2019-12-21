using AutoMapper;

using Conglomerate.Api.Models;
using Conglomerate.Data.Entities.SandwichShop;
using Conglomerate.ServiceRepository.Models;

namespace Conglomerate.App.Configuration
{
    public class ConfigureAutoMapper : Profile
    {
        public ConfigureAutoMapper()
        {
            // Will be better once Lamar is added
            CreateMap<Ingredient, IngredientLogic>();
            CreateMap<IngredientLogic, Ingredient>();
            CreateMap<IngredientLogic, IngredientDto>();
            CreateMap<IngredientDto, IngredientLogic>();
        }
    }
}
