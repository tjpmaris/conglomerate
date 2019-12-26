using AutoMapper;

using Conglomerate.Cqrs.Queries;
using Conglomerate.Data.Entities.SandwichShop;

namespace Conglomerate.Cqrs.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ingredient, IngredientGetAll.Ingredient>();
            CreateMap<Ingredient, IngredientGetById.Ingredient>();
            CreateMap<Ingredient, IngredientCreate.Ingredient>();
            CreateMap<Ingredient, IngredientUpdate.Ingredient>();
            CreateMap<Ingredient, IngredientDelete.Ingredient>();
        }
    }
}
