using FluentValidation;

namespace Application.UseCases.V1.Authentication.Commands.SignUp;
public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().MaximumLength(250);
        RuleFor(c => c.LastName).NotEmpty().MaximumLength(250);
        RuleFor(c => c.Email).NotEmpty().MaximumLength(10).EmailAddress();
        RuleFor(c => c.Username).NotEmpty().MaximumLength(250);
        RuleFor(c => c.Password).NotEmpty().MaximumLength(250);
        RuleFor(c => c.PasswordConfirm).NotEmpty().MaximumLength(250).Matches(c => c.Password);
    }
}
