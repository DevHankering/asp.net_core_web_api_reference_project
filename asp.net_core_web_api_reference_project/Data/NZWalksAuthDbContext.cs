using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace asp.net_core_web_api_reference_project.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }

        //data seeding
            protected override void OnModelCreating(ModelBuilder builder)
            {
                    base.OnModelCreating(builder);

                    var readerRoleId = "8893e4ac-c21f-4aee-99b2-b094b07fdb5a";
                    var writerRoleId = "8a6c0bfb-5f2c-41d6-a470-e78f70646443";

            var roles = new List<IdentityRole>
                        {
                            new IdentityRole
                            {
                                 Id = readerRoleId,    //this id is a string but since we are using Guid, we will convert it into an string
                                 ConcurrencyStamp = readerRoleId,
                                 Name = "Reader",          // this is name of this role
                                 NormalizedName = "Reader".ToUpper()
                            },
                            new IdentityRole
                            {
                                Id = writerRoleId,
                                ConcurrencyStamp = writerRoleId,
                                Name = "Writer",
                                NormalizedName = "Writer".ToUpper()
                            }
                        };

            // Now we will seed this inside the builder object
            builder.Entity<IdentityRole>().HasData(roles);
        }
        
    }
}
