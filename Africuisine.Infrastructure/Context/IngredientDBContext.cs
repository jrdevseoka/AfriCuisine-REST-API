using Africuisine.Domain;
using Africuisine.Domain.Ingredients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Africuisine.Infrastructure.Context
{
    public class IngredientDBContext : DbContext
    {
        private readonly ISave Save;
        public DbSet<IngredientDM> Ingredients { get; set; }
        public DbSet<IngredientCategoryDM> IngredientCategories { get; set; }

        public IngredientDBContext(DbContextOptions<IngredientDBContext> options, ISave save)
            : base(options)
        {
            Save = save;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> entities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            Save.GenerateBaseModelData(entities, cancellationToken);
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
