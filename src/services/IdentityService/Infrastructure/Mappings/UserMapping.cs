using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.Property(u => u.Id)
                  .HasColumnName("id")
                  .HasDefaultValue(0)
                  .IsRequired(true);
                  //.UseIdentityAlwaysColumn()

            user.Property(u => u.Name)
                .HasColumnName("name")
                .HasDefaultValue("incognito")
                .HasMaxLength(50)
                .IsRequired(true);

            user.Property(u => u.Email)
                .HasColumnName("email")
                .HasDefaultValue(null)
                .HasMaxLength(50)
                .IsRequired(false);

            user.Property(u => u.Password)
                .HasColumnName("password")
                .HasMaxLength(50)
                .IsRequired(true);

            user.Property(u => u.Login)
                .HasColumnName("login")
                .HasMaxLength(50)
                .IsRequired(true);
        }
    }
}
