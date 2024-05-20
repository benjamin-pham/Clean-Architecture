using System.ComponentModel.DataAnnotations;

namespace Domain.EntityRules;
public class CommonRules
{
    public static Func<string, bool> IsValidEmail = input => new EmailAddressAttribute().IsValid(input);
}
