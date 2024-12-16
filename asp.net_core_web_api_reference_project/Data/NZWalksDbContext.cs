using asp.net_core_web_api_reference_project.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net_core_web_api_reference_project.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
        {
                
        }

       public DbSet<Difficulty> Difficulties { get; set; }
       public DbSet<Region> Regions { get; set; }
       public DbSet<Walk> Walks { get; set; }



        //Data seeding

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data from Difficulties
            //Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new()   // in old time, new Difficulty()   was written but now only new() will work just fine
                {
                    Id = Guid.Parse("cd4e2ab8-c12e-4ecd-aa38-2cb05f65f518"),
                    Name = "Easy"
                },
                new()
                {
                    Id = Guid.Parse("e8cfd57f-247e-416a-bd2c-7c71dd0ccc32"),
                    Name = "Medium"
                },
                new()
                {
                    Id = Guid.Parse("10d6c1fe-e783-4649-88b6-a1d895c04a34"),
                    Name = "Hard"
                }
            };

            //Seed dificulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            var regions = new List<Region>()
            {
                new()
                {
                    Id = Guid.Parse("51b3adc3-d4b1-4e3b-8d94-a3d623b56c82"),
                    Name = "RAEBARELI",
                    Code = "RBL",
                    RegionImageUrl = "https://cdn.s3waas.gov.in/s3e3796ae838835da0b6f6ea37bcf8bcb7/uploads/2018/07/2018072687.jpg"
                },
                new()
                {
                    Id = Guid.Parse("f347a2a8-978b-4c5a-89c4-62f01ed716f2"),
                    Name = "LUCKNOW",
                    Code = "LKO",
                    RegionImageUrl = "https://t4.ftcdn.net/jpg/05/13/77/31/240_F_513773104_G7Pin2bxWwpMAWqI5MIvrSnWDpYs80WN.jpg"
                },
                new()
                {
                    Id = Guid.Parse("757c096c-97e9-4cb1-8b80-f212b140c12c"),
                    Name = "DELHI",
                    Code = "DL",
                    RegionImageUrl = "https://deih43ym53wif.cloudfront.net/Rajpath-delhi-shutterstock_1195751923.jpg_7647e1aad2.jpg"
                },
                new()
                {
                    Id = Guid.Parse("2570dcb7-1bd1-47b1-bc02-8bbcd82f2d08"),
                    Name = "NOIDA",
                    Code = "NOIDA"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions); 
        }
    }
}
 