using Application.Constants;

namespace Application.DTOs.Filters;
/// <summary>
/// thôn tin tìm kiếm chung
/// </summary>
public class BaseSearch
{
    private const int _pageIndexDefault = PagedConstant.PAGE_INDEX_DEFAULT;
    private const int _pageSizeDefault = PagedConstant.PAGE_SIZE_DEFAULT;
    /// <summary>
    /// vị trí trang
    /// </summary>
    public int PageIndex
    {
        get => _pageIndex;
        set { if (value <= 0) _pageIndex = _pageIndexDefault; else _pageIndex = value; }
    }
    private int _pageIndex = _pageIndexDefault;
    /// <summary>
    /// kích thước trang
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set { if (value <= 0) _pageSize = _pageSizeDefault; else _pageSize = value;  }
    }
    private int _pageSize = _pageSizeDefault;
    /// <summary>
    /// thông tin tìm kiếm
    /// </summary>
    public string SearchTerm { get; set; } = null;
    /// <summary>
    /// sắp xếp theo cột
    /// </summary>
    public string SortColumn { get; set; }
    /// <summary>
    /// thứ tự sắp xếp tăng hay giảm
    /// </summary>
    public string SortOrder { get; set; }
}