using Domain.EntityRules;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;
public class UserConfiguration : BaseEntityConfiguration<User, Guid>, IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        this.EntityCommondCreating(builder);

        builder.HasIndex(entity => entity.Username).IsUnique(true);
        builder.HasIndex(entity => entity.Email).IsUnique(true);

        builder.Property(entity => entity.PasswordHash)
            .IsUnicode(UserRules.HashPassword_IsUnicode)
            .IsRequired(UserRules.HashPassword_IsRequired)
            .HasMaxLength(UserRules.HashPassword_MaxLength);

        builder.Property(entity => entity.FirstName)
            .IsUnicode(UserRules.FirstName_IsUnicode)
            .IsRequired(UserRules.FirstName_IsRequired)
            .HasMaxLength(UserRules.FirstName_MaxLength);

        builder.Property(entity => entity.LastName)
           .IsUnicode(UserRules.LastName_IsUnicode)
           .IsRequired(UserRules.LastName_IsRequired)
           .HasMaxLength(UserRules.LastName_MaxLength);

        builder.Property(entity => entity.Email)
           .IsUnicode(UserRules.Email_IsUnicode)
           .IsRequired(UserRules.Email_IsRequired)
           .HasMaxLength(UserRules.Email_MaxLength);

        builder.Property(entity => entity.Username)
           .IsUnicode(UserRules.Username_IsUnicode)
           .IsRequired(UserRules.Username_IsRequired)
           .HasMaxLength(UserRules.Username_MaxLength);

        builder.Property(entity => entity.PhoneNumber)
           .IsUnicode(UserRules.PhoneNumber_IsUnicode)
           .IsRequired(UserRules.PhoneNumber_IsRequired)
           .HasMaxLength(UserRules.PhoneNumber_MaxLength);

    }
}
