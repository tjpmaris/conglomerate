using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Conglomerate.Data.Contexts;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Conglomerate.CQRS.Queries
{
    public static class IngredientGetById
    {
        public class Query : IRequest<Ingredient>
        {
            public int Id { get; set; }
        }

        public class Ingredient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

        public class Handler : IRequestHandler<Query, Ingredient>
        {
            private readonly SandwichShopContext _context;
            private readonly IMapper _mapper;

            public Handler(SandwichShopContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Ingredient> Handle(Query query, CancellationToken cancellationToken)
            {
                return await _context
                    .Ingredients
                    .ProjectTo<Ingredient>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(s => s.Id == query.Id);
            }
        }
    }
}
