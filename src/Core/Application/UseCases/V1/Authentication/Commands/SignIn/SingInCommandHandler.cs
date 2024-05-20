using Application.Abstractions.Message;
using Application.DTOs.Authentication;
using Application.Libraries;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.V1.Authentication.Commands.SignIn;
public sealed class SingInCommandHandler : BaseCommandHandler,
    ICommandHandler<SignInCommand, TokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtHandler _jwtHandler;
    public SingInCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userRepository = serviceProvider.GetService<IUserRepository>();
        _jwtHandler = serviceProvider.GetService<IJwtHandler>();
    }

    public async Task<Result<TokenResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        string hashPassword = _userRepository.HashPassword(request.Password);
        User user = await _userRepository.LoginVerificationAsync(request.Username, hashPassword);

        if (user == null)
        {
            Result.Failure("cannot verify");
        }

        TokenResponse token = _jwtHandler.GenerateToken(user.Id.ToString());

        return Result.Success(token);
    }
}
