using Application.DTOs.Authentication;
using Application.Libraries;
using Application.UseCases.V1.Authentication.Commands.SignIn;
using Application.UseCases.V1.Authentication.Commands.SignUp;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers.V1;

/// <summary>
/// xác thực người dùng
/// </summary>
public class AuthController : ApiV1Controller
{
    /// <summary>
    /// đăng nhập
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("sign-in")]
    public Task<Result<TokenResponse>> SignIn([FromBody] SignInCommand request) => Sender.Send(request);
    /// <summary>
    /// đăng ký
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("sign-up")]
    public Task<Result> SignUp([FromBody] SignUpCommand request) => Sender.Send(request);
}
