using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrashGrounds.File.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.File.Models.Main.Abstractions;

namespace TrashGrounds.File.Database.Postgres.Configurations;

public abstract class BaseFileConfiguration<TEntity> : BaseConfiguration<TEntity>
    where TEntity : BaseFile
{
    public override void ConfigureChild(EntityTypeBuilder<TEntity> typeBuilder)
    {
        typeBuilder.Property(file => file.UploadDate)
            .HasDefaultValueSql("NOW()");
    }
}