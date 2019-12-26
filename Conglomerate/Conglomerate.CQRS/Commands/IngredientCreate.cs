using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Conglomerate.Data.Contexts;

using MediatR;

namespace Conglomerate.CQRS.Queries
{
    public static class IngredientCreate
    {
        public class Command : IRequest<Ingredient>
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
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
                var ingredient = new Data.Entities.SandwichShop.Ingredient
                {
                    Name = command.Name,
                    Price = command.Price
                };

                _context.Ingredients.Add(ingredient);
                await _context.SaveChangesAsync();

                return _mapper.Map<Ingredient>(ingredient);
            }
        }
    }
}
