using Application.DTOs.Responses;
using Application.Libraries;
using Application.UseCases.V1.Profiles.Commands.UpdatePassword;
using Application.UseCases.V1.Profiles.Commands.UpdateProfile;
using Application.UseCases.V1.Profiles.Queries.GetProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers.V1;
/// <summary>
/// thông tin người dùng (đang đăng nhập)
/// </summary>
[Authorize]
public class ProfileController : ApiV1Controller
{
    /// <summary>
    /// lấy thông tin người dùng
    /// </summary>
    /// <returns></returns>
    [HttpGet("user-context")]
    [AppAuthorize]
    public Task<Result<ProfileResponse>> GetProfile() => Sender.Send(new GetProfileQuery());
    /// <summary>
    /// cập nhật thông tin người dùng
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("user-context")]
    [AppAuthorize]
    public Task<Result> UpdateProfile([FromBody] UpdateProfileCommand request) => Sender.Send(request);
    /// <summary>
    /// cập nhật mật khẩu
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("user-context/password")]
    [AppAuthorize]
    public Task<Result> UpdatePassword([FromBody] UpdatePasswordCommand request) => Sender.Send(request);
}