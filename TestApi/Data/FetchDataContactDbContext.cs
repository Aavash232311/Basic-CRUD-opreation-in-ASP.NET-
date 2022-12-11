using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Data
{
    public class FetchDataContactDbContext: DbContext
    {
  
        public FetchDataContactDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Contact> UserInfo { get; set; }

    }
}
