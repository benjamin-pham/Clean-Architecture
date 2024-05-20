using Application.Abstractions.Message;
using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Libraries;
using Application.Libraries.AppMapper;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.V1.Profiles.Queries.GetProfile;
public sealed class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, ProfileResponse>
{
    private readonly IUserRepository _userRepository;
    public GetProfileQueryHandler(IServiceProvider serviceProvider)
    {
        _userRepository = serviceProvider.GetService<IUserRepository>();
    }
    public async Task<Result<ProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.FindByIdAsync(UserContext.Instance.Id);
        AppException.CheckDataExist(user, UserContext.Instance.Id);
        return Result.Success(ModelMapper.Map<ProfileResponse>(user));
    }
}
