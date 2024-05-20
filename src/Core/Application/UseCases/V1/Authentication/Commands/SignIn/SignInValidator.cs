using FluentValidation;

namespace Application.UseCases.V1.Authentication.Commands.SignIn;
internal class SignInValidator : AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(c => c.Username).NotEmpty().MaximumLength(250);
        RuleFor(c => c.Password).NotEmpty().MaximumLength(250);
    }
}
