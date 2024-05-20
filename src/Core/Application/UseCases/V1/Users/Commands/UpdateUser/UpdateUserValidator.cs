using Application.Services.RulesForDTO;
using FluentValidation;

namespace Application.UseCases.V1.Users.Commands.UpdateUser;
public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RulesForUserDTO<UpdateUserCommand>.FirstName(RuleFor(x => x.FirstName));

        RulesForUserDTO<UpdateUserCommand>.LastName(RuleFor(x => x.LastName));

        RulesForUserDTO<UpdateUserCommand>.Email(RuleFor(x => x.Email));

        RulesForUserDTO<UpdateUserCommand>.PhoneNumber(RuleFor(x => x.PhoneNumber));

        RulesForUserDTO<UpdateUserCommand>.Username(RuleFor(x => x.Username));
    }
}
