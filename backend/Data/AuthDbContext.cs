using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        // will be used by Identity framework
    }
}
