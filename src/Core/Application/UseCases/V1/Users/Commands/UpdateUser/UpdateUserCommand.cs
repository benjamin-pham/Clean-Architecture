using Application.Abstractions.Message;

namespace Application.UseCases.V1.Users.Commands.UpdateUser;
public class UpdateUserCommand : ICommand
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
