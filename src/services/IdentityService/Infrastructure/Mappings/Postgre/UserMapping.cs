using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Mappings.Postgre
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.ToTable("user");
            user.HasKey(x => x.Id);

            user.Property(u => u.Id)
                 .HasColumnName("id")
                 .HasColumnType("uuid")
                 .HasDefaultValueSql("uuid_generate_v4()")
                 .IsRequired(true);
            //.UseIdentityAlwaysColumn()

            user.Property(u => u.Name)
                .HasColumnName("name")
                .HasColumnType("varchar")
                .HasDefaultValue("incognito")
                .HasMaxLength(50)
                .IsRequired(true);

            user.Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("varchar")
                .HasDefaultValue(null)
                .HasMaxLength(50)
                .IsRequired(false);

            user.Property(u => u.Password)
                .HasColumnName("password")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(true);

            user.Property(u => u.Login).HasColumnName("login")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(true);

            user.Ignore(u => u.CreateTime);
        }
    }
}
