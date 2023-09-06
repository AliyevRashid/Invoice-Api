namespace Invoice_Api.DTO.Pagination;

public class PaginationList<TModel>
{
    public IEnumerable<TModel> Items { get; }
    public PaginationMeta Meta { get; }

    public PaginationList(IEnumerable<TModel> items, PaginationMeta meta)
    {
        Items = items;
        Meta = meta;
    }
}
