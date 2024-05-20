using Application.Abstractions.Message;
using Application.Exceptions;
using Application.Libraries;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.V1.Profiles.Commands.UpdatePassword;
public sealed class UpdatePasswordCommandHandler : BaseCommandHandler, ICommandHandler<UpdatePasswordCommand>
{
    private readonly IUserRepository _userRepository;
    public UpdatePasswordCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userRepository = serviceProvider.GetService<IUserRepository>();
    }

    public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.FindByIdAsync(UserContext.Id);
        AppException.CheckDataExist(user, UserContext.Id);

        string oldPasswordHash = _userRepository.HashPassword(request.OldPassword);

        if (oldPasswordHash != user.PasswordHash)
        {
            Result.Failure();
        }

        string newPasswordHash = _userRepository.HashPassword(request.NewPassword);

        user.PasswordHash = newPasswordHash;

        _userRepository.Update(user, user => user.PasswordHash);

        return Result.Success();
    }
}
