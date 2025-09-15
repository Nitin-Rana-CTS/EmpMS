using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        // will be used by Identity framework

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var EmployeeRoleId = "ema850f27d-35b2-5335-b8cf-2cc11c8d7904";
            var AdminRoleId = "ada850f27d-35b2-5335-b8cf-2cc11c8d1234";

            // creating roles 

            var roles = new List<IdentityRole>
            {
               
                new IdentityRole
                {
                    Id = EmployeeRoleId,
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                    ConcurrencyStamp = EmployeeRoleId
                },
                new IdentityRole
                {
                    Id = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = AdminRoleId
                }
            };

            //seeding the roles
            builder.Entity<IdentityRole>().HasData(roles);


            // Create an Admin User
            var adminUserId = "auif3d378fd-e54d-5f4c-9219-b2b2f92a017e";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@ems.com",
                Email = "admin@ems.com",
                NormalizedEmail = "admin@ems.com".ToUpper(),
                NormalizedUserName = "admin@ems.com".ToUpper()
            };

            admin.PasswordHash = "AQAAAAIAAYagAAAAEN8bW++TcWy/Cr+a+6OPHLvelczErtPz2YgrkhzoQtuSgQC+af3Z24jcaM5/yutWVQ=="; 

            builder.Entity<IdentityUser>().HasData(admin);


            // Define composite key for IdentityUserRole
            builder.Entity<IdentityUserRole<string>>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });


            // Giving roles to Admin
            var adminUserRole = new IdentityUserRole<string>
            {
                UserId = adminUserId,
                RoleId = AdminRoleId
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);
        }
    }
}
