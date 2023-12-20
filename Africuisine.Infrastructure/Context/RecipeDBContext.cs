using Africuisine.Domain.Models.Recipes;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Infrastructure.Context
{
    internal class RecipeDBContext : DbContext
    {
        private readonly ISave Save;
        public DbSet<RecipeDM> Recipes { get; set; }
        public DbSet<RecipeCategoryDM> RecipeCategories { get; set; }
        public DbSet<InstructionDM> Instructions { get; set; }

        public RecipeDBContext(ISave save)
            : base()
        {
            Save = save;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Save.GenerateBaseModelData()
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
