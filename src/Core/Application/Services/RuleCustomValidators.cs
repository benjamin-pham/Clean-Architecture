using FluentValidation;

namespace Application.Services;
public static class RuleCustomValidators
{
    public static IRuleBuilder<T, TProperty> IsRequired<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, bool isRequired = true)
    {
        if (isRequired)
        {
            ruleBuilder.NotEmpty().NotNull();
        }
        return ruleBuilder;
    }

    public static IRuleBuilder<T, P> IsUnicode<T, P>(this IRuleBuilder<T, P> ruleBuilder, bool isUnicde = true)
    {
        return ruleBuilder;
    }
}