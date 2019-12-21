using System.Collections.Generic;
using System.Threading.Tasks;

using Conglomerate.Data.Contexts;
using Conglomerate.Data.Entities.SandwichShop;

using Microsoft.EntityFrameworkCore;

namespace Conglomerate.ServiceRepository.Repositories
{
    public interface IIngredientRepository
    {
        Task<IList<Ingredient>> GetAll();
        Task<Ingredient> Get(int id);
        Task<int> Create(Ingredient ingredient);
        Task<Ingredient> Update(int id, Ingredient ingredient);
        Task<Ingredient> Delete(int id);
    }

    public class IngredientRepository : IIngredientRepository
    {
        private readonly SandwichShopContext _context;

        public IngredientRepository(SandwichShopContext context)
        {
            _context = context;
        }

        public async Task<IList<Ingredient>> GetAll()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<Ingredient> Get(int id)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> Create(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return ingredient.Id;
        }

        public async Task<Ingredient> Update(int id, Ingredient ingredient)
        {
            var update = await Get(id);

            update.Name = ingredient.Name;
            update.Price = ingredient.Price;

            _context.Ingredients.Update(update);
            await _context.SaveChangesAsync();

            return update;
        }

        public async Task<Ingredient> Delete(int id)
        {
            var ingredient = await Get(id);
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return ingredient;
        }
    }
}
