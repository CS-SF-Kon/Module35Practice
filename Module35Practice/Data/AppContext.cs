using Module35Practice.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Module35Practice.Configs;

namespace Module35Practice.Data;

public class AppContext : IdentityDbContext<User>
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration<Friend>(new FriendConfiguration());
    }
}
