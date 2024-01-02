using Africuisine.Application.Interfaces.Utils;
using Africuisine.Domain.Models.Pictures;
using Africuisine.Domain.Models.Recipes;
using Africuisine.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Africuisine.Infrastructure.Context
{
    public class RecipeDBContext : DbContext
    {
        private readonly ISave Save;
        public DbSet<RecipeDM> Recipes { get; set; }
        public DbSet<RecipePictureDM> RecipePictures { get; set; }  
        public DbSet<PictureDM> Pictures { get; set; }  
        public DbSet<RecipeCategoryDM> RecipeCategories { get; set; }
        public DbSet<RecipeLevelDM> RecipeLevels { get; set; }
        public DbSet<InstructionDM> Instructions { get; set; }

        public RecipeDBContext(ISave save, DbContextOptions<RecipeDBContext> options)
            : base(options)
        {
            Save = save;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedRecipeLevels();
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> entities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            Save.GenerateBaseModelData(entities, cancellationToken);
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
