namespace CVU.CONDICA.Dto.Pagination
{
    public class PaginatedQueryParameter
    {
        private const int MaxPageSize = 200;
        private int _pageSize = 10;
        public int Page { get; set; } = 1;
        public int ItemsPerPage
        {
            get => _pageSize;

            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
