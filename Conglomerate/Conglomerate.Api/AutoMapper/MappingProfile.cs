using AutoMapper;

using Conglomerate.Api.Models;
using Conglomerate.ServiceRepository.Models;

namespace Conglomerate.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<IngredientLogic, IngredientDto>();
            CreateMap<IngredientDto, IngredientLogic>();
        }
    }
}
