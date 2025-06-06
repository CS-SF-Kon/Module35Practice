﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module35Practice.Models.Users;

namespace Module35Practice.Configs;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("Messages").HasKey(p => p.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
    }
}
