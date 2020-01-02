using System;
using System.Threading.Tasks;

using Conglomerate.Data.Contexts;

using Microsoft.EntityFrameworkCore;

namespace Conglomerate.Process.OneTime
{
    public class OneTimeProcess
    {
        private readonly SandwichShopContext _context;

        public OneTimeProcess(SandwichShopContext context)
        {
            _context = context;
        }

        public async Task Execute()
        {
            var count = await _context.Ingredients.CountAsync();
            Console.WriteLine($"Number of ingredients: {count}");
        }
    }
}
