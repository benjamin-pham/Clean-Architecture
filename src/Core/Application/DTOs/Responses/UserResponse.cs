namespace Application.DTOs.Responses;
public class UserResponse : BaseEntityResponse<Guid>
{
    /// <summary>
    /// tên
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// họ
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// tên người dùng
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// thư điện tử
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// số điện thoại
    /// </summary>
    public string PhoneNumber { get; set; }
}
