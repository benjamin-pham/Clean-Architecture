using Application.Abstractions.Message;
using Application.DTOs.Filters;
using Application.DTOs.Responses;
using Application.Libraries;

namespace Application.UseCases.V1.Users.Queries.GetPagedListUser;
public class GetPagedListUserQuery : BaseSearch, IQuery<PagedList<UserResponse>>
{

}
