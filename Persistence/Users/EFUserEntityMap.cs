using Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Users
{
    public class EFUserEntityMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(_ => _.Id);

            builder.OwnsOne(f => f.FullName, f =>
            {
                f.Property(fn => fn.FirstName)
                    .HasMaxLength(Domain.SharedKernel.FirstName.MaxLenght)
                    .HasColumnName(nameof(Domain.SharedKernel.FullName.FirstName))
                    .HasConversion(f => f.Value, f =>
                        Domain.SharedKernel.FirstName.Create(f).Value);

                f.Property(ln => ln.LastName)
                    .HasMaxLength(Domain.SharedKernel.LastName.MaxLenght)
                    .HasColumnName(nameof(Domain.SharedKernel.FullName.LastName))
                    .HasConversion(f => f.Value, f =>
                        Domain.SharedKernel.LastName.Create(f).Value);
            });

            builder.Property(_ => _.UserName)
                .HasMaxLength(Domain.Aggregates.Users.ValueObjects.UserName.MaxLenght)
                .HasColumnName(nameof(Domain.Aggregates.Users.User.UserName))
                .HasConversion(f => f.Value, f =>
                    Domain.Aggregates.Users.ValueObjects.UserName.Create(f).Value);
        }
    }
}