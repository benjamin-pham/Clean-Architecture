namespace Application.DTOs;
public abstract class BaseEntityResponse<TKey>
{
    /// <summary>
    /// khóa chính
    /// </summary>
    public TKey Id { get; set; }
    /// <summary>
    /// ngày tạo
    /// </summary>
    public DateTimeOffset CreatedOn { get; set; }
    /// <summary>
    /// ngày cập nhật
    /// </summary>
    public DateTimeOffset? UpdatedOn { get; set; }
    /// <summary>
    /// người tạo
    /// </summary>
    public Guid? CreatedBy { get; set; }
    /// <summary>
    /// người cập nhật
    /// </summary>
    public Guid? UpdatedBy { get; set; }
}
