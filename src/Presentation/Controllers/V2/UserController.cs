using Application.DTOs.Responses;
using Application.Libraries;
using Application.UseCases.V1.Users.Commands.CreateUser;
using Application.UseCases.V1.Users.Commands.DeleteUser;
using Application.UseCases.V1.Users.Commands.UpdateUser;
using Application.UseCases.V1.Users.Queries.GetPagedListUser;
using Application.UseCases.V1.Users.Queries.GetUserById;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;

namespace Presentation.Controllers.V2;
[ApiVersion(2)]
public class UserController : ApiController
{
    /// <summary>
    /// lấy danh sách người dùng phân trang
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public Task<Result<PagedList<UserResponse>>> GetPagedList([FromQuery] GetPagedListUserQuery request) => Sender.Send(request);
    /// <summary>
    /// lấy thông tin người dùng theo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public Task<Result<UserResponse>> GetById(Guid id) => Sender.Send(new GetUserByIdQuery { Id = id });
    /// <summary>
    /// tạo người dùng
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public Task<Result> Create([FromBody] CreateUserCommand request) => Sender.Send(request);
    /// <summary>
    /// cập nhật người dùng
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<Result> Update(Guid id, [FromBody] UpdateUserCommand request)
    {
        request.Id = id;
        return await Sender.Send(request);
    }
    /// <summary>
    /// xóa người dùng
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public Task<Result> Delete(Guid id) => Sender.Send(new DeleteUserCommand { Id = id });
}
