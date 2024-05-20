using Application.Abstractions.Message;
using Application.DTOs.Responses;
using Application.Libraries;
using Domain.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Application.UseCases.V1.Users.Queries.GetPagedListUser;
public sealed class GetPagedListUserQueryHandler : BaseQueryHandler<GetPagedListUserQuery, UserResponse>,
    IQueryHandler<GetPagedListUserQuery, PagedList<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    public GetPagedListUserQueryHandler(IServiceProvider serviceProvider)
    {
        SortColumns = [CreatedOn, UpdatedOn, FirstName, LastName, Email, PhoneNumber, UserName];
        _userRepository = serviceProvider.GetService<IUserRepository>();
    }
    private const string FirstName = "firstname";
    private const string LastName = "lastname";
    private const string Email = "email";
    private const string PhoneNumber = "phonenumber";
    private const string UserName = "username";
    public async Task<Result<PagedList<UserResponse>>> Handle(GetPagedListUserQuery request, CancellationToken cancellationToken)
    {
        var query = _userRepository.AsQueryable().Select(user => new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            UserName = user.Username,
            CreatedBy = user.CreatedBy,
            CreatedOn = user.CreatedOn,
            UpdatedBy = user.UpdatedBy,
            UpdatedOn = user.UpdatedOn,
        });

        query = this.ApplyPredicate(query, request);

        query = this.ApplySort(query, request.SortColumn, request.SortOrder);

        //PagedList<UserResponse> result =
        //    await PagedList<UserResponse>.CreateAsync(query, request.PageIndex, request.PageSize);
        PagedList<UserResponse> result = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);

        return Result.Success(result);
    }

    protected override List<Expression<Func<UserResponse, bool>>> BuildPredicate(GetPagedListUserQuery queryModel)
    {
        List<Expression<Func<UserResponse, bool>>> predicate = new List<Expression<Func<UserResponse, bool>>>();
        if (!string.IsNullOrEmpty(queryModel.SearchTerm))
        {
            predicate.Add(user => user.FirstName.Contains(queryModel.SearchTerm)
            || user.LastName.Contains(queryModel.SearchTerm)
            || user.Email.Contains(queryModel.SearchTerm)
            || user.PhoneNumber.Contains(queryModel.SearchTerm)
            || user.UserName.Contains(queryModel.SearchTerm));
        }

        return predicate;
    }


    protected override Expression<Func<UserResponse, object>> BuildSort(string sortColumn)
    {
        return sortColumn switch
        {
            FirstName => user => user.FirstName,
            LastName => user => user.LastName,
            Email => user => user.Email,
            PhoneNumber => user => user.PhoneNumber,
            UserName => user => user.UserName,
            CreatedOn => user => user.CreatedOn,
            UpdatedOn => user => user.UpdatedOn,
            _ => user => user.CreatedOn
        };
    }
}
