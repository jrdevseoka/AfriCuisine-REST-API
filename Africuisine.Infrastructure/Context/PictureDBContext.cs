using Africuisine.Domain.Models.Pictures;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Infrastructure.Context
{
    public class PictureDBContext : DbContext
    {
        public DbSet<PictureDM> Pictures { get; set; }
        public DbSet<ProfilePictureDM> ProfilePictures { get; set; }
        public PictureDBContext(DbContextOptions<PictureDBContext> options)
            : base(options) { }
    }
}
