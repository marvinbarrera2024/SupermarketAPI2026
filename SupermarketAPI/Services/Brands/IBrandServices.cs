using SupermarketAPI.DTOs;

namespace SupermarketAPI.Services.Brands
{
    public interface IBrandServices
    {
        Task<int> PostBrand(BrandRequest brand);
        Task<List<BrandResponse>> GetBrands();
        Task<BrandResponse?> GetBrand(int brandId);
        Task<int> PutBrand(int brandId, BrandRequest brand);
        Task<int> DeleteBrand(int brandId);
    }
}