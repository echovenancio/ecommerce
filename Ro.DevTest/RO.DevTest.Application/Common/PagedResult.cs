namespace RO.DevTest.Application.Common;

public record PagedResult<T>(
    int TotalCount,
    int PageSize,
    int PageNumber,
    IEnumerable<T> Items
);
