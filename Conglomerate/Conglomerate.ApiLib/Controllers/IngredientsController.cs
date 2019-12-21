using System.Threading.Tasks;

using Conglomerate.Data.Contexts;
using Conglomerate.Data.Entities.SandwichShop;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Conglomerate.ApiLib
{
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            using var context = new SandwichShopContext();

            var ingredients = await context.Ingredients.ToListAsync();

            return Ok(ingredients);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            using var context = new SandwichShopContext();

            var ingredient = await context.Ingredients.FirstAsync(s => s.Id == id);

            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ingredient ingredient)
        {
            using var context = new SandwichShopContext();

            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();

            return Created("", ingredient.Id);
        }
    }
}
