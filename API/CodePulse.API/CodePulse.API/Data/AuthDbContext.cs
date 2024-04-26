using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Guid.NewGuid().ToString();
            var readerRoleId = "4d9815da-4ab6-4006-9103-2b5159b5ea99";
            var writerRoleId = "900e9183-466b-4690-8084-472532191218";

            //Create reader and writer role
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            //Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //Create an Admin User
            var adminUserId = "a94a24d3-fa77-4b0d-80e7-828e191f42d6";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()

            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
            builder.Entity<IdentityUser>().HasData(admin);

            //Give Roles To Admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }

}
