using Application.Abstractions.Message;
using Application.Libraries;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.V1.Users.Commands.CreateUser;
public sealed class CreateUserCommandHandler : BaseCommandHandler,
    ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;
    public CreateUserCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userRepository = serviceProvider.GetService<IUserRepository>();
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = new User()
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = _userRepository.HashPassword(request.Password)
        };

        await _userRepository.CheckInfoIsUniqueAsync(user);

        _userRepository.Add(user);

        return Result.Success();
    }
}