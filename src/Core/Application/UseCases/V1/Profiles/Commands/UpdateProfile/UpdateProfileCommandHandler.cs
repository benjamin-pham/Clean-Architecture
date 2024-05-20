using Application.Abstractions.Message;
using Application.Exceptions;
using Application.Libraries;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.V1.Profiles.Commands.UpdateProfile;
public sealed class UpdateProfileCommandHandler : BaseCommandHandler, ICommandHandler<UpdateProfileCommand>
{
    private readonly IUserRepository _userRepository;
    public UpdateProfileCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userRepository = serviceProvider.GetService<IUserRepository>();
    }

    public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.FindByIdAsync(UserContext.Id);
        AppException.CheckDataExist(user, UserContext.Id);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Username = request.Username;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;

        await _userRepository.CheckInfoIsUniqueAsync(user);

        _userRepository.Update(user);

        return Result.Success();
    }
}