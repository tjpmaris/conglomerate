using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Conglomerate.Data.Contexts;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Conglomerate.CQRS.Queries
{
    public static class IngredientDelete
    {
        public class Command : IRequest<Ingredient>
        {
            public int Id { get; set; }
        }

        public class Ingredient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

        public class Handler : IRequestHandler<Command, Ingredient>
        {
            private readonly SandwichShopContext _context;
            private readonly IMapper _mapper;

            public Handler(SandwichShopContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Ingredient> Handle(Command command, CancellationToken cancellationToken)
            {
                var ingredient =
                    await _context
                        .Ingredients
                        .FirstOrDefaultAsync(s => s.Id == command.Id);

                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();

                return _mapper.Map<Ingredient>(ingredient);
            }
        }
    }
}
