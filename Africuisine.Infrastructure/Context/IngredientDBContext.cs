using Microsoft.EntityFrameworkCore;

namespace Africuisine.Infrastructure.Context
{
    public class IngredientDBContext : DbContext
    {
        public IngredientDBContext(DbContextOptions<IngredientDBContext> options)
            : base(options) { }

    }
}
