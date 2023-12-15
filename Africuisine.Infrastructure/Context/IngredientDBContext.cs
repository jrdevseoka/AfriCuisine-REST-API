using Africuisine.Domain;
using Africuisine.Domain.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Infrastructure.Context
{
    public class IngredientDBContext : DbContext
    {
        public DbSet<IngredientDM> Ingredients { get; set; }
        public DbSet<IngredientCategoryDM> IngredientCategories { get; set; }

        public IngredientDBContext(DbContextOptions<IngredientDBContext> options)
            : base(options) { }
    }
}
