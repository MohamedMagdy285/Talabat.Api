namespace Talab.Core.Helpers
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        private const int MaxPageSize = 10;

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

        public int PageIndex { get; set; } = 1;

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value> MaxPageSize? MaxPageSize : value; }
        }

    }
}
