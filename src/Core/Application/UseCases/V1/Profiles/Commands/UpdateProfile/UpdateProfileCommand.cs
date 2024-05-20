
using Application.Abstractions.Message;

namespace Application.UseCases.V1.Profiles.Commands.UpdateProfile;
/// <summary>
/// cập nhật thông tin
/// </summary>
public sealed record UpdateProfileCommand : ICommand
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
    /// số điện thoại
    /// </summary>
    public string PhoneNumber { get; set; }
}
