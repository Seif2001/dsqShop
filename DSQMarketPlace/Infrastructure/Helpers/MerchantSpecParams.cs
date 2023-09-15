namespace Infrastructure.Helpers
{
    public class MerchantSpecParams
    {
        private const int maxPageSize = 50;

        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize { get { return _pageSize; } set { _pageSize = (value > maxPageSize) ? maxPageSize : value; } }
        private string _search;
        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
