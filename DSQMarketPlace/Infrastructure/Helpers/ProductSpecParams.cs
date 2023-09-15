namespace Infrastructure.Helpers
{
    public class ProductSpecParams
    {
        private const int maxPageSize = 50;

        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;

        public int PageSize { get { return _pageSize; } set { _pageSize = (value > maxPageSize) ? maxPageSize : value; } }
        public int? MerchantId { get; set; }
        public string? Sort { get; set; }
       

    }
}
