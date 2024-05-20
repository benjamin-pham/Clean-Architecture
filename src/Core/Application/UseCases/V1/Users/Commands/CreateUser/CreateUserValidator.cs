using Application.Services;
using Application.Services.RulesForDTO;
using FluentValidation;

namespace Application.UseCases.V1.Users.Commands.CreateUser;
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RulesForUserDTO<CreateUserCommand>.FirstName(RuleFor(x => x.FirstName));

        RulesForUserDTO<CreateUserCommand>.LastName(RuleFor(x => x.LastName));

        RulesForUserDTO<CreateUserCommand>.Email(RuleFor(x => x.Email));

        RulesForUserDTO<CreateUserCommand>.PhoneNumber(RuleFor(x => x.PhoneNumber));

        RulesForUserDTO<CreateUserCommand>.Password(RuleFor(x => x.Password));

        RulesForUserDTO<CreateUserCommand>.Username(RuleFor(x => x.Username));        
    }
}