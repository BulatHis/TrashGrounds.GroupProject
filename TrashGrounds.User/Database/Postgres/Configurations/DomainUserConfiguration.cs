using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrashGrounds.User.Database.Postgres.Configurations.Abstractions;
using TrashGrounds.User.Models.Main;

namespace TrashGrounds.User.Database.Postgres.Configurations;

public class DomainUserConfiguration : BaseConfiguration<DomainUser>
{
    public override void ConfigureChild(EntityTypeBuilder<DomainUser> typeBuilder)
    {
        typeBuilder.HasIndex(user => user.IdentityUserId);
    }
}