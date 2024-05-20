using Domain.Abstractions.Entities;

namespace Domain.Entities;
/// <summary>
/// người dùng
/// </summary>
public class User : BaseEntity
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
    /// tên tài khoản
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// mật khẩu
    /// </summary>
    public string PasswordHash { get; set; }
    /// <summary>
    /// thư điện tử
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// số điện thoại
    /// </summary>
    public string PhoneNumber { get; set; }


}
