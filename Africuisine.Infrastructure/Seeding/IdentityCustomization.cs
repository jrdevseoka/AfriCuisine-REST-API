using Africuisine.Domain.Models;
using Africuisine.Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Infrastructure.Seeding
{
   public static class IdentityCustomization
    {
        public static ModelBuilder IdentityUserCustomization(this ModelBuilder builder)
        {
            builder.Entity<UserDM>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);

                // Indexes for "normalized" username and email, to allow efficient lookups
                b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("IX_User_NormalizedName");
                b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("IX_User_NormailizedEmail").IsUnique();

                // Maps to the AspNetUsers table
                b.ToTable("USERS");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);

                // The relationships between User and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each User can have many UserClaims
                b.HasMany<UserClaimDM>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

                // Each User can have many UserLogins
                b.HasMany<UserLoginDM>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

                // Each User can have many UserTokens
                b.HasMany<UserTokenDM>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany<UserRoleDM>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });
            builder.Entity<UserClaimDM>(b =>
            {
                // Primary key
                b.HasKey(uc => uc.Id);

                // Maps to the AspNetUserClaims table
                b.ToTable("USERCLAIMS");
            });
            builder.Entity<UserLoginDM>(b =>
            {
                // Composite primary key consisting of the LoginProvider and the key to use
                // with that provider
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);

                // Maps to the AspNetUserLogins table
                b.ToTable("USERLOGINS");
            });
            builder.Entity<UserTokenDM>(b =>
            {
                // Composite primary key consisting of the UserId, LoginProvider and Name
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

                // Limit the size of the composite key columns due to common DB restrictions
                b.Property(t => t.LoginProvider).HasMaxLength(255);
                b.Property(t => t.Name).HasMaxLength(255);

                // Maps to the AspNetUserTokens table
                b.ToTable("USERTOKENS");
            });

            return builder;
        }
        public static ModelBuilder RoleIdentityCustomization(this ModelBuilder builder)
        {

            builder.Entity<RoleDM>(b =>
            {
                // Primary key
                b.HasKey(r => r.Id);

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

                // Maps to the AspNetRoles table
                b.ToTable("ROLES");

                // A concurrency token for use with the optimistic concurrency checking
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                // The relationships between Role and other entity types
                // Note that these relationships are configured with no navigation properties

                // Each Role can have many entries in the UserRole join table
                b.HasMany<UserRoleDM>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany<RoleClaimDM>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            });
            builder.Entity<RoleClaimDM>(b =>
            {
                // Primary key
                b.HasKey(rc => rc.Id);

                // Maps to the AspNetRoleClaims table
                b.ToTable("ROLECLAIMS");
            });

            builder.Entity<UserRoleDM>(b =>
            {
                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });

                // Maps to the AspNetUserRoles table
                b.ToTable("USERROLES");
            });
            return builder;
        }
    }
}
