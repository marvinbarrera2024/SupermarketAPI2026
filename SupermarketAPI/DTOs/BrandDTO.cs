namespace SupermarketAPI.DTOs
{
    public class BrandResponse
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public bool BrandStatus { get; set; }
    }

    public class BrandRequest
    {
        public string BrandName { get; set; } = null!;

        public bool BrandStatus { get; set; }
    }
}