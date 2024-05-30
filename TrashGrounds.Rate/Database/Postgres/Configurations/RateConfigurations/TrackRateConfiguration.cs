using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrashGrounds.Rate.Models.Main;

namespace TrashGrounds.Rate.Database.Postgres.Configurations.RateConfigurations;

public class TrackRateConfiguration : BaseRateConfiguration<TrackRate>
{
    public override void ConfigureChild(EntityTypeBuilder<TrackRate> builder)
    {
        builder.HasIndex(rate => new {rate.UserId, rate.TrackId})
            .IsDescending(false, false)
            .IsUnique();

        builder.HasIndex(rate => rate.TrackId);
    }
}