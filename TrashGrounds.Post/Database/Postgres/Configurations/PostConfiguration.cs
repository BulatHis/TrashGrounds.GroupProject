using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrashGrounds.Post.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.Post.Database.Postgres.Configurations;

public class PostConfiguration : DependencyInjectedEntityConfiguration<Models.Main.Post>
{
    public override void Configure(EntityTypeBuilder<Models.Main.Post> builder)
    {
        builder.Property(post => post.UploadDate)
            .HasDefaultValueSql("NOW()");

        builder.HasIndex(post => post.UserId);
    }
}