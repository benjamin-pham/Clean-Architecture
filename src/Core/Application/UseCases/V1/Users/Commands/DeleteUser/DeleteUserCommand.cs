using Application.Abstractions.Message;

namespace Application.UseCases.V1.Users.Commands.DeleteUser;
public class DeleteUserCommand : ICommand
{
    public Guid Id { get; set; }
}
