namespace ViewModels.Response
{
    public class TransactionListResponse
    {
        public List<TransactionResponse> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
