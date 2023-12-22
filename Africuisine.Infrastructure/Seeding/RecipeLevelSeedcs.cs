using Africuisine.Domain.Enum;
using Africuisine.Domain.Models.Recipes;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Infrastructure.Seeding
{
     public static class RecipeLevelSeedcs
    {
        public static ModelBuilder SeedRecipeLevels(this ModelBuilder builder)
        {
            builder.Entity<RecipeLevelDM>(b =>
            {
               
                b.Property( r => r.Name ).IsRequired();
                b.HasIndex(u => u.Name).HasDatabaseName("IX_RecDifficulty_Name").IsUnique();
                b.Property(r => r.Link).ValueGeneratedOnAdd();
                b.Property(u => u.Creation).ValueGeneratedOnAdd();
                b.Property(u => u.LastUpdate).ValueGeneratedOnAddOrUpdate();
                var levels = GenerateRecipeLevels().ToArray();
                b.HasData(levels);
                
            });
            return builder;
        }
        private static List<RecipeLevelDM> GenerateRecipeLevels()
        {
            List<RecipeLevelDM> recipeLevels = new();
            var names =  Enum.GetNames(typeof(ERecipeLevel));
            foreach (string name in names)
            {
                var level = new RecipeLevelDM {  Name = name, Creation = DateTime.Now, LastUpdate = DateTime.Now, Link = Guid.NewGuid().ToString() };
                recipeLevels.Add(level);
            }
            return recipeLevels;
        }
    }
}
