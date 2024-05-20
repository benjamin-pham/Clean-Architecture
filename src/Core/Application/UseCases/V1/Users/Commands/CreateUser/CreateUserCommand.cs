using Application.Abstractions.Message;

namespace Application.UseCases.V1.Users.Commands.CreateUser;
/// <summary>
/// tạo user
/// </summary>
public class CreateUserCommand : ICommand
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
    public string Username { get; set; }
    /// <summary>
    /// thư điện thử
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// số điện thoại
    /// </summary>
    public string PhoneNumber { get; set; }
    /// <summary>
    /// mật khẩu
    /// </summary>
    public string Password { get; set; }
}