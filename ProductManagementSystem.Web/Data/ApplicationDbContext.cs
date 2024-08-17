using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductManagementSystem.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole
                    {
                        Id = "6f42fdf1-b675-4b8f-8a04-e8aa6d405fc7",
                        Name = "Employee",
                        NormalizedName = "EMPLOYEE"
                    },
                    new IdentityRole
                    {
                        Id = "68dd38db-50d7-4480-90c0-29913a649f77",
                        Name = "Supervisor",
                        NormalizedName = "SUPERVISOR"
                    },
                    new IdentityRole
                    {
                        Id = "17bb7d8a-885b-4df9-9c9e-b1ea79b056aa",
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    }
                );

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData
                (
                    new ApplicationUser
                    {
                        Id = "ffc16461-f454-4d61-af1b-86252f0a2703",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "ADMIN@LOCALHOST.COM",
                        NormalizedUserName = "ADMIN@LOCALHOST.COM",
                        UserName = "admin@localhost.com",
                        PasswordHash = passwordHasher.HashPassword(null, "P@ssword1"),
                        EmailConfirmed = true,
                        FirstName = "Default",
                        LastName = "Admin",
                        DateOfBirth = new DateOnly(1950,12,12)
                    }
                );

            builder.Entity<IdentityUserRole<string>>().HasData
                (
                    new IdentityUserRole<string>
                    {
                        RoleId = "17bb7d8a-885b-4df9-9c9e-b1ea79b056aa",
                        UserId = "ffc16461-f454-4d61-af1b-86252f0a2703"
                    }
                );
        }

        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
