using Africuisine.Domain.Models.Pictures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Africuisine.Infrastructure.Context
{
    public class PictureDBContext : DbContext
    {
        private  ISave Save { get; set; }
        public DbSet<PictureDM> Pictures { get; set; }
        public DbSet<ProfilePictureDM> ProfilePictures { get; set; }

        public PictureDBContext(DbContextOptions<PictureDBContext> options, ISave save)
            : base(options)
        {
            Save = save;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry> entities = ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            Save.GenerateBaseModelData(entities, cancellationToken);
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
