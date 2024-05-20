using Application.Constants;

namespace Application.Libraries;
public class LanguageContext
{
    public static string Instance
    {
        get
        {
            string language = (string)ApplicationHttpContext.HttpContext.Items["Language"];
            if (!Language.Languages.Contains(language))
            {
                language = Language.en;
            }
            return language;
        }
    }
}
