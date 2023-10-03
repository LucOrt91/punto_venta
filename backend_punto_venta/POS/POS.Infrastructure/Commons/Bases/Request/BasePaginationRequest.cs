namespace POS.Infrastructure.Commons.Bases.Request
{
    public class BasePaginationRequest
    {
        public int NumPage { get; set; } = 1;
        public int NumRecordsPage { get; set; } = 10;

        private readonly int NumMaxRecords = 50;
        public string Order { get; set; } = "asc";
        public string? Sort { get; set; } = null;

        public int Recods
        {
            get => NumMaxRecords;
            set
            {
                NumRecordsPage = value > NumMaxRecords ? NumMaxRecords : value;
            }
        }

    }
}
