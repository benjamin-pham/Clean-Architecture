using Application.Abstractions.Message;
using Application.Libraries;
using Application.Libraries.AppMapper;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.V1.Authentication.Commands.SignUp;
public sealed class SignUpCommandHandler : BaseCommandHandler,
    ICommandHandler<SignUpCommand>
{
    private readonly IUserRepository _userRepository;
    public SignUpCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userRepository = serviceProvider.GetService<IUserRepository>();
    }

    public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        User user = ModelMapper.Map<User>(request.Username);
        user.Id = Guid.NewGuid();
        user.PasswordHash = _userRepository.HashPassword(request.Password);

        await _userRepository.CheckInfoIsUniqueAsync(user);

        _userRepository.Add(user);

        return Result.Success();
    }
}