using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module35Practice.Models.Users;

namespace Module35Practice.Configs;

public class FriendConfiguration : IEntityTypeConfiguration<Friend>
{
    public void Configure(EntityTypeBuilder<Friend> builder)
    {
        builder.ToTable("UserFriends").HasKey(p => p.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
    }
}
