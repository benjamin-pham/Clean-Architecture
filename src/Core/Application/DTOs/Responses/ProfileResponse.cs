namespace Application.DTOs.Responses;
public class ProfileResponse
{
    /// <summary>
    /// khóa chính
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// tên
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// họ
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// thư điện tử
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// số điện thoại
    /// </summary>
    public string PhoneNumber { get; set; }
}
