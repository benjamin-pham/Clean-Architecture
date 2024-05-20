using Application.Abstractions.Message;
using Application.DTOs.Authentication;

namespace Application.UseCases.V1.Authentication.Commands.SignIn;
public class SignInCommand : ICommand<TokenResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}
