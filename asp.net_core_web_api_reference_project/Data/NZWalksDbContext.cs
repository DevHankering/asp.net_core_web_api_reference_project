using asp.net_core_web_api_reference_project.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net_core_web_api_reference_project.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
                
        }

       public DbSet<Difficulty> Difficulties { get; set; }
       public DbSet<Region> Regions { get; set; }
       public DbSet<Walk> Walks { get; set; }
    }
}
 