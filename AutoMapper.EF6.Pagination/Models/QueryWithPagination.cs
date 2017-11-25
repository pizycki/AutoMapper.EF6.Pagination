namespace AutoMapper.EF6.Pagination.Models
{
    public interface IPaginationInfo
    {
        int Page { get; }
        int PageSize { get; }
    }

    public interface ISortingInfo
    {
        string OrderBy { get; set; }
        bool Descending { get; set; }
    }

    public interface IQueryWithPagination : IPaginationInfo, ISortingInfo { }
}
