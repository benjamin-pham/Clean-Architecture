using Domain.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public abstract class BaseEntityConfiguration<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public void EntityCommondCreating(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(typeof(TEntity).Name);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedBy).IsRequired(false);
        builder.Property(x => x.CreatedOn).IsRequired(true);
        builder.Property(x => x.UpdatedBy).IsRequired(false);
        builder.Property(x => x.UpdatedOn).IsRequired(false);
        builder.Property(x => x.IsDeleted).IsRequired(true);
        builder.Property(x => x.DeletedBy).IsRequired(false);
        builder.Property(x => x.DeletedOn).IsRequired(false);
    }
}