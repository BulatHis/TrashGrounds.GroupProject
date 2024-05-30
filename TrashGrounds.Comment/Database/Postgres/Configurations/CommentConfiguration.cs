using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrashGrounds.Comment.Database.Postgres.Configurations.Abstractions;

namespace TrashGrounds.Comment.Database.Postgres.Configurations;

public class CommentConfiguration : DependencyInjectedEntityConfiguration<Models.Main.Comment>
{
    public override void Configure(EntityTypeBuilder<Models.Main.Comment> builder)
    {
        builder.Property(comment => comment.SendAt)
            .HasDefaultValueSql("NOW()");

        builder.HasIndex(comment => comment.TrackId);
    }
}