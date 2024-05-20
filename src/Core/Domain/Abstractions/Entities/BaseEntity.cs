namespace Domain.Abstractions.Entities;
public class BaseEntity<TKey>
{
    public TKey Id { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset? UpdatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }
}
public class BaseEntity : BaseEntity<Guid>
{
}