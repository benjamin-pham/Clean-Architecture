using Application.Abstractions.Message;

namespace Application.UseCases.V1.Authentication.Commands.SignUp;
/// <summary>
/// đăng ký tài khoản
/// </summary>
public class SignUpCommand : ICommand
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
    /// thư điện tử
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// mật khẩu
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// xác nhận mật khẩu
    /// </summary>
    public string PasswordConfirm { get; set; }
}
