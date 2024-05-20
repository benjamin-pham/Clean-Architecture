using Application.Constants;
using Microsoft.EntityFrameworkCore;

namespace Application.Libraries;
/// <summary>
/// dữ liệu danh sách phân trang
/// </summary>
/// <typeparam name="TItem">danh sách dữ liệu</typeparam>
public class PagedList<TItem> where TItem : class
{
    private const int _pageIndexDefault = PagedConstant.PAGE_INDEX_DEFAULT;
    private const int _pageSizeDefault = PagedConstant.PAGE_SIZE_DEFAULT;

    /// <summary>
    /// vị trí trang
    /// </summary>
    public int PageIndex
    {
        get => _pageIndex;
        //set { if (value <= 0) _pageIndex = _pageIndexDefault; else _pageIndex = value; }
    }

    private int _pageIndex = _pageIndexDefault;

    /// <summary>
    /// kích thước trang
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        //set { if (value <= 0) _pageSize = _pageSizeDefault; else _pageSize = value; }
    }

    private int _pageSize = _pageSizeDefault;

    /// <summary>
    /// tổng kết quả
    /// </summary>
    public int TotalResults { get; } = 0;

    /// <summary>
    /// tổng trang
    /// </summary>
    public int TotalPages
    {
        get => (int)Math.Ceiling((float)TotalResults / PageSize);
    }

    /// <summary>
    /// Has previous page
    /// </summary>
    public bool HasPreviousPage { get; }

    /// <summary>
    /// Has next age
    /// </summary>
    public bool HasNextPage { get; }

    /// <summary>
    /// danh sách dữ liệu
    /// </summary>
    public IList<TItem> Items { get; }

    public PagedList(IList<TItem> items, int pageIndex, int pageSize, int totalResult)
    {
        Items = items;
        _pageIndex = pageIndex;
        _pageSize = pageSize;
        TotalResults = totalResult;
    }

    /// <summary>
    /// tạo dữ liệu phân trang từ IQueryable
    /// </summary>
    public static async Task<PagedList<TItem>> CreateAsync(IQueryable<TItem> query, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        pageIndex = pageIndex <= 0 ? _pageIndexDefault : pageIndex;
        pageSize = pageSize <= 0 ? _pageSizeDefault : pageSize;
        var totalResuls = await query.CountAsync(cancellationToken);
        if (totalResuls > 0)
        {
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var items = await query.ToListAsync(cancellationToken);
            return new(items, pageIndex, pageSize, totalResuls);
        }
        return new PagedList<TItem>(Array.Empty<TItem>(), 0, 0, 0);
    }
}

public static class PagedListQueryableExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken token = default) where T : class
    => await PagedList<T>.CreateAsync(source, pageIndex, pageSize, token);
}
