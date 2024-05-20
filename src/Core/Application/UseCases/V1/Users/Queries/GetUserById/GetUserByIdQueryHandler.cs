using Application.Abstractions.Message;
using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Libraries;
using Application.Libraries.AppMapper;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.V1.Users.Queries.GetUserById;
public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdQueryHandler(IServiceProvider serviceProvider)
    {
        _userRepository = serviceProvider.GetService<IUserRepository>();
    }
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.FindByIdAsync(request.Id);
        AppException.CheckDataExist(user, request.Id);
        return Result.Success(ModelMapper.Map<UserResponse>(user));
    }
}
