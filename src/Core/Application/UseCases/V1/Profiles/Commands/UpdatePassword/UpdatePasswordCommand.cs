using Application.Abstractions.Message;

namespace Application.UseCases.V1.Profiles.Commands.UpdatePassword;
public class UpdatePasswordCommand : ICommand
{
    /// <summary>
    /// mật khẩu cũ
    /// </summary>
    public string OldPassword { get; set; }
    /// <summary>
    /// mật khẩu mới
    /// </summary>
    public string NewPassword { get; set; }
    /// <summary>
    /// xác nhận mật khẩu
    /// </summary>
    public string PasswordConfirm { get; set; }
}
