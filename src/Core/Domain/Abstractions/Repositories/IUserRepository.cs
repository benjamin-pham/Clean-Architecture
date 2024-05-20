using Domain.Abstractions.BaseRepositories;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;
public interface IUserRepository : IBaseRepository<User, Guid>
{
    /// <summary>
    /// mã hóa mật khẩu
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    string HashPassword(string password);
    /// <summary>
    /// lấy thông tin người dùng đăng nhập
    /// </summary>
    /// <param name="username">tên tài khoản</param>
    /// <param name="passwordHash">mật khẩu</param>
    /// <returns></returns>
    Task<User> LoginVerificationAsync(string username, string passwordHash);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="username"></param>
    /// <param name="email"></param>
    /// <returns>true if the info is unique</returns>
    Task CheckInfoIsUniqueAsync(User userInfo);
}
