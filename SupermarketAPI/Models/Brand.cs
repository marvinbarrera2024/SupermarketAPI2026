namespace SupermarketAPI.Models;

public class Brand
{
    public int BrandId { get; set; }

    public string BrandName { get; set; } = null!;

    public bool BrandStatus { get; set; }
}