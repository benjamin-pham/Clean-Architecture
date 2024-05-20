using Domain.EntityRules;
using FluentValidation;

namespace Application.Services.RulesForDTO;
public class RulesForUserDTO<T>
{
    public static void FirstName(IRuleBuilderInitial<T, string> ruleBuilderOptions)
    {
        ruleBuilderOptions
            .MinimumLength(UserRules.FirstName_MinLength)
            .MaximumLength(UserRules.FirstName_MaxLength)
            .IsRequired(UserRules.FirstName_IsRequired)
            .IsUnicode(UserRules.FirstName_IsUnicode);
    }

    public static void LastName(IRuleBuilderInitial<T, string> ruleBuilderOptions)
    {
        ruleBuilderOptions
            .MinimumLength(UserRules.LastName_MinLength)
            .MaximumLength(UserRules.LastName_MaxLength)
            .IsRequired(UserRules.LastName_IsRequired)
            .IsUnicode(UserRules.LastName_IsUnicode);
    }

    public static void Email(IRuleBuilderInitial<T, string> ruleBuilderOptions)
    {
        ruleBuilderOptions
            .EmailAddress()
            .MaximumLength(UserRules.Email_MaxLength)
            .IsRequired(UserRules.Email_IsRequired)
            .IsUnicode(UserRules.Email_IsUnicode);
    }

    public static void Username(IRuleBuilderInitial<T, string> ruleBuilderOptions)
    {
        ruleBuilderOptions
            .Must(UserRules.IsValidUsername).WithMessage($"'Username' is not well formatted")
            .MinimumLength(UserRules.Username_MinLength)
            .MaximumLength(UserRules.Username_MaxLength)
            .IsRequired(UserRules.Username_IsRequired)
            .IsUnicode(UserRules.Username_IsUnicode);
    }

    public static void PhoneNumber(IRuleBuilderInitial<T, string> ruleBuilderOptions)
    {
        ruleBuilderOptions
            .MaximumLength(UserRules.PhoneNumber_MaxLength)
            .IsRequired(UserRules.PhoneNumber_IsRequired)
            .IsUnicode(UserRules.PhoneNumber_IsUnicode);
    }

    public static void Password(IRuleBuilderInitial<T, string> ruleBuilderOptions)
    {
        ruleBuilderOptions
            .MinimumLength(UserRules.Password_MinLength)
            .MaximumLength(UserRules.Password_MaxLength)
            .IsRequired(UserRules.PhoneNumber_IsRequired)
            .IsUnicode(UserRules.Password_IsUnicode);
    }
}
