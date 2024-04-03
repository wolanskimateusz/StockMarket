namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        //Filtering
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;

        //Pagination

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
