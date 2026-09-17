using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupermarketAPI.DTOs;
using SupermarketAPI.Models;

namespace SupermarketAPI.Services.Brands
{
    public class BrandServices : IBrandServices
    {
        private readonly SupermarketDbContext _db;
        private readonly IMapper _mapper;

        public BrandServices(SupermarketDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<int> DeleteBrand(int brandId)
        {
            var brand = await _db.Brands.FindAsync(brandId);
            if (brand == null)
                return -1;

            _db.Brands.Remove(brand);
            return await _db.SaveChangesAsync();
        }

        public async Task<BrandResponse?> GetBrand(int brandId)
        {
            var brand = await _db.Brands.FindAsync(brandId);
            return brand == null ? null : _mapper.Map<Brand, BrandResponse>(brand);
        }

        public async Task<List<BrandResponse>> GetBrands()
        {
            var brands = await _db.Brands.ToListAsync();
            return _mapper.Map<List<Brand>, List<BrandResponse>>(brands);
        }

        public async Task<int> PostBrand(BrandRequest brand)
        {
            var entity = _mapper.Map<BrandRequest, Brand>(brand);
            await _db.Brands.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity.BrandId;
        }

        public async Task<int> PutBrand(int brandId, BrandRequest brand)
        {
            var entity = await _db.Brands.FindAsync(brandId);
            if (entity == null)
                return -1;

            entity.BrandName = brand.BrandName;
            entity.BrandStatus = brand.BrandStatus;
            _db.Brands.Update(entity);

            return await _db.SaveChangesAsync();
        }
    }
}