namespace CVU.CONDICA.Dto.Pagination
{
    public class PaginatedModel<T>
    {
        public PaginatedModel()
        {

        }

        public PaginatedModel(IEnumerable<T> list, PaginatedHeaderParameter pageSummary)
        {
            Items = list;
            PagedSummary = pageSummary;
        }
        public IEnumerable<T> Items { get; set; }
        public PaginatedHeaderParameter PagedSummary { get; set; }
    }
}
