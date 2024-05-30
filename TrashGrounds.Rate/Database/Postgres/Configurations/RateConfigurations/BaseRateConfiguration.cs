using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrashGrounds.Rate.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.Rate.Models.Main.Abstractions;

namespace TrashGrounds.Rate.Database.Postgres.Configurations.RateConfigurations;

public abstract class BaseRateConfiguration<TEntity> : BaseConfiguration<TEntity>
    where TEntity : BaseRate
{
    public override void ConfigureChild(EntityTypeBuilder<TEntity> typeBuilder)
    {
        typeBuilder.Property(rate => rate.DateTime)
            .HasDefaultValueSql("NOW()");
    }
}