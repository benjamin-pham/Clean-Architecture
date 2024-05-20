using Application.Abstractions.Message;
using Application.Constants;
using Application.Exceptions;
using Application.Libraries;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.V1.Users.Commands.DeleteUser;
public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    public DeleteUserCommandHandler(IServiceProvider serviceProvider)
    {
        _userRepository = serviceProvider.GetService<IUserRepository>();
    }
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.FindByIdAsync(request.Id);
        AppException.CheckDataExist(user, request.Id);

        #region check reference
        #endregion

        _userRepository.Delete(user);

        return Result.Success();
    }
}
