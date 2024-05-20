namespace Application.Constants;
public class SortOrder
{
    public const string Asc = "asc";
    public const string Desc = "desc";
    public static string[] SortOrders = [Asc, Desc];
    public enum SortOrderType
    {
        Asc = 1,
        Desc = 2
    }
    public static string GetSortOrder(SortOrderType key)
    {
        string name = Enum.GetName(typeof(SortOrderType), key).ToLower();
        return SortOrders.Where(x => x.Equals(name)).SingleOrDefault()?.ToString();
    }
    public static SortOrderType GetSortOrder(string title)
    {
        SortOrderType sortOrderType = 0;
        try
        {
            sortOrderType = (SortOrderType)Enum.Parse(typeof(SortOrderType), title.First().ToString().ToUpper() + title.Substring(1), true);
        }catch { }
        return sortOrderType;
    }
}