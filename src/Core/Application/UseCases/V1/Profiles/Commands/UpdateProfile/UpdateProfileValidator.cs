using FluentValidation;

namespace Application.UseCases.V1.Profiles.Commands.UpdateProfile;
public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().MaximumLength(250);
        RuleFor(c => c.LastName).NotEmpty().MaximumLength(250);
        RuleFor(c => c.Email).NotEmpty().MaximumLength(10).EmailAddress();
        RuleFor(c => c.PhoneNumber).MaximumLength(250);
        RuleFor(c => c.Username).NotEmpty().MaximumLength(250);
    }
}
