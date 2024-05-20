using System.Text.RegularExpressions;

namespace Domain.EntityRules;
public class UserRules
{
    #region FirstName
    public const int FirstName_MaxLength = 255;
    public const int FirstName_MinLength = 1;
    public const bool FirstName_IsRequired = true;
    public const bool FirstName_IsUnicode = true;
    #endregion

    #region LastName
    public const int LastName_MaxLength = 255;
    public const int LastName_MinLength = 1;
    public const bool LastName_IsRequired = true;
    public const bool LastName_IsUnicode = true;
    #endregion

    #region Email
    public const int Email_MaxLength = 320;
    public const int Email_MinLength = 3;
    public const bool Email_IsRequired = true;
    public const bool Email_IsUnicode = false;

    public static Func<string, bool> IsValidEmail = CommonRules.IsValidEmail;
    #endregion

    #region Username
    public static readonly Func<string, bool> IsValidUsername =
        input => Regex.IsMatch(input, @"(?!.*_{2,})^[a-zA-Z][a-zA-Z0-9_]*[a-zA-Z0-9]$");

    public const int Username_MaxLength = 64;
    public const int Username_MinLength = 6;
    public const bool Username_IsRequired = true;
    public const bool Username_IsUnicode = false;
    #endregion

    #region PhoneNumber
    public const int PhoneNumber_MaxLength = 20;
    public const bool PhoneNumber_IsRequired = false;
    public const bool PhoneNumber_IsUnicode = false;
    #endregion

    #region Password
    public const int HashPassword_MaxLength = 250;
    public const bool HashPassword_IsRequired = true;
    public const bool HashPassword_IsUnicode = false;

    public const int Password_MaxLength = 64;
    public const int Password_MinLength = 8;
    public const bool Password_IsRequired = HashPassword_IsRequired;
    public const bool Password_IsUnicode = HashPassword_IsUnicode;
    #endregion
}
