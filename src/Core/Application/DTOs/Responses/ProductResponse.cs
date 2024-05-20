namespace Application.DTOs.Responses;
public record ProductResponse
{
    /// <summary>
    /// khóa chính
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// tên sản phẩm
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// mã sản phẩm
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// giá tiền
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// ngày tạo
    /// </summary>
    public DateTimeOffset CreatedOn { get; set; }
    /// <summary>
    /// ngày cập nhật
    /// </summary>
    public DateTimeOffset? UpdatedOn { get; set; }
}
