using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrashGrounds.Rate.Models.Main;

namespace TrashGrounds.Rate.Database.Postgres.Configurations.RateConfigurations;

public class PostRateConfiguration : BaseRateConfiguration<PostRate>
{
    public override void ConfigureChild(EntityTypeBuilder<PostRate> builder)
    {
        builder.HasIndex(rate => new {rate.UserId, rate.PostId})
            .IsDescending(false, false)
            .IsUnique();

        builder.HasIndex(rate => rate.PostId);
    }
}