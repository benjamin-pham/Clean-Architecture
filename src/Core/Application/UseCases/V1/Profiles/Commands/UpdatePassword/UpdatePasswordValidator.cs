using FluentValidation;

namespace Application.UseCases.V1.Profiles.Commands.UpdatePassword;
public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordValidator()
    {
        RuleFor(c => c.OldPassword).NotEmpty().MaximumLength(250);
        RuleFor(c => c.NewPassword).NotEmpty().MaximumLength(250);
        RuleFor(c => c.PasswordConfirm).NotEmpty().MaximumLength(250).Matches(c => c.NewPassword);
    }
}