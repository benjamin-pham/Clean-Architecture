using Application.Constants;
using Application.Libraries;
using System.Linq.Expressions;
using static Application.Constants.SortOrder;

namespace Application.Abstractions.Message;
public abstract class BaseQueryHandler<TQuery, TQueryResponse>
{
    protected const string CreatedOn = "createdon";
    protected const string UpdatedOn = "updatedon";
    private string[] _sortColumns = [];
    private SortOrderType _sortOrderDefault;
    protected virtual string[] SortColumns { set { _sortColumns = value; } }
    protected virtual SortOrderType SortOrderDefault { set { _sortOrderDefault = value; } }
    /// <summary>
    /// áp dụng điều kiện sắp xếp
    /// </summary>
    /// <param name="query"></param>
    /// <param name="sortColumn"></param>
    /// <param name="sortOrder"></param>
    /// <returns></returns>
    public IQueryable<TQueryResponse> ApplySort(IQueryable<TQueryResponse> query, string sortColumn, string sortOrder)
    {
        sortColumn = (sortColumn ?? "").ToLower();

        sortOrder = (sortOrder ?? "").ToLower();

        bool hasColumn = true;

        if (_sortColumns == null || _sortColumns.Length == 0)
        {
            Result.Failure("Sort columns is empty");
        }

        if (!_sortColumns.Contains(sortColumn))
        {
            hasColumn = false;
        }        

        if (!hasColumn)
        {
            var existsSortOrder = Enum.IsDefined(typeof(SortOrderType), _sortOrderDefault);
            if (!existsSortOrder)
            {
                sortOrder = SortOrder.Desc;
            }
            else
            {
                sortOrder = SortOrder.GetSortOrder(_sortOrderDefault);
            }
        }

        var keySelector = this.BuildSort(sortColumn);

        if (sortOrder == SortOrder.Asc)
        {
            query = query.OrderBy(keySelector);
        }
        else
        {
            query = query.OrderByDescending(keySelector);
        }
        return query;
    }
    /// <summary>
    /// áp dụng truy vấn
    /// </summary>
    /// <param name="query"></param>
    /// <param name="queryModel"></param>
    /// <returns></returns>
    public IQueryable<TQueryResponse> ApplyPredicate(IQueryable<TQueryResponse> query, TQuery queryModel)
    {
        var predicates = BuildPredicate(queryModel);
        for (int i = 0; i < predicates.Count; i++)
        {
            query = query.Where(predicates[i]);
        }
        return query;
    }
    /// <summary>
    /// xây dựng điều kiện sắp xếp
    /// </summary>
    /// <param name="sortColumn"></param>
    /// <returns></returns>
    protected abstract Expression<Func<TQueryResponse, object>> BuildSort(string sortColumn);
    /// <summary>
    /// xây dựng truy vấn
    /// </summary>
    /// <param name="queryModel"></param>
    /// <returns></returns>
    protected abstract List<Expression<Func<TQueryResponse, bool>>> BuildPredicate(TQuery queryModel);
}