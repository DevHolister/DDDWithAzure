namespace Linde.Core.Coaching.Common.Models;

public record PaginatedListDto<T>(
    int Total,
    int PageSize,
    int Page,
    IEnumerable<T> Items)
{
    public int TotalPages { get => ComputeTotalPages(); }

    private int ComputeTotalPages()
    {
        if (PageSize > 0)
        {
            return (int)Math.Ceiling(Total / (double)PageSize);
        }
        return 1;
    }
}
