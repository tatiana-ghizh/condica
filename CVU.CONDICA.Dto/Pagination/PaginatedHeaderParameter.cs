﻿namespace CVU.CONDICA.Dto.Pagination
{
    public class PaginatedHeaderParameter
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
