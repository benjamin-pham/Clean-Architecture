using Application.Abstractions.Message;
using Application.DTOs.Responses;

namespace Application.UseCases.V1.Users.Queries.GetUserById;
public class GetUserByIdQuery : IQuery<UserResponse>
{
    public Guid Id { get; set; }
}
