using CVU.CONDICA.Dto.Pagination;

namespace CVU.CONDICA.Client.Services
{
    public class PaginationManager<TModel, TQuery> where TQuery : PaginatedQueryParameter
    {
        public IEnumerable<TModel> Items { get; set; }
        public TQuery QueryModel { get; set; }
        public PaginatedHeaderParameter PageDetails { get; private set; }
        public bool Searching { get; private set; }


        private readonly BlazorHttpClient httpClient;

        private string url;



        public PaginationManager(BlazorHttpClient httpClient)
        {
            this.httpClient = httpClient;

            QueryModel = Activator.CreateInstance<TQuery>();

        }

        public void SetRequestUrl(string url)
        {
            this.url = url;
        }
        public async Task PageChanged(int requestedPage)
        {
            QueryModel.Page = requestedPage;

            await Search(false);
        }

        public async Task Search(bool resetPage = true)
        {
            Items = Enumerable.Empty<TModel>();

            Searching = true;

            if (resetPage)
            {
                QueryModel.Page = 1;
            }

            try
            {
                var queryString = QueryModel.ToQueryStringg();

                var response = await httpClient.Get<PaginatedModel<TModel>>($"{url}?{queryString}");

                if (response != null)
                {
                    Items = response.Items;
                    PageDetails = response.PagedSummary;
                }
            }
            finally
            {
                Searching = false;
            }
        }

        //public async Task PageChanged(int requestedPage)
        //{
        //    QueryModel.Page = requestedPage;

        //    await Search();
        //}

        //public async Task Search()
        //{
        //    Items = Enumerable.Empty<TModel>();

        //    Searching = true;

        //    try
        //    {
        //        var response = await httpClient.Get<PaginatedModel<TModel>>($"{url}?{QueryModel.ToQueryString()}");

        //        if (response != null)
        //        {
        //            Items = response.Items;
        //            PageDetails = response.PagedSummary;
        //        }
        //    }
        //    finally
        //    {
        //        Searching = false;
        //    }
        //}
    }
}
