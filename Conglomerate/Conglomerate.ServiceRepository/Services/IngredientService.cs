using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Conglomerate.Data.Entities.SandwichShop;
using Conglomerate.ServiceRepository.Models;
using Conglomerate.ServiceRepository.Repositories;

namespace Conglomerate.ServiceRepository.Services
{
    public interface IIngredientService
    {
        Task<IList<IngredientLogic>> GetAll();
        Task<IngredientLogic> Get(int id);
        Task<int> Create(IngredientLogic ingredient);
        Task<IngredientLogic> Update(int id, IngredientLogic ingredient);
        Task<IngredientLogic> Delete(int id);
    }

    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<IList<IngredientLogic>> GetAll()
        {
            return _mapper.Map<List<IngredientLogic>>(await _ingredientRepository.GetAll());
        }

        public async Task<IngredientLogic> Get(int id)
        {
            return _mapper.Map<IngredientLogic>(await _ingredientRepository.Get(id));
        }

        public async Task<int> Create(IngredientLogic ingredient)
        {
            return await _ingredientRepository.Create(_mapper.Map<Ingredient>(ingredient));
        }

        public async Task<IngredientLogic> Update(int id, IngredientLogic ingredient)
        {
            return _mapper.Map<IngredientLogic>(await _ingredientRepository.Update(id, _mapper.Map<Ingredient>(ingredient)));
        }

        public async Task<IngredientLogic> Delete(int id)
        {
            return _mapper.Map<IngredientLogic>(await _ingredientRepository.Delete(id));
        }
    }
}
