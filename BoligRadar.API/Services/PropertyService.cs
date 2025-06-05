using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BoligRadar.API.Data;
using BoligRadar.API.DTO;
using BoligRadar.API.Models;

namespace BoligRadar.API.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PropertyService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PropertyDto>> GetPropertiesAsync(decimal? minPrice, decimal? maxPrice, int? minSize, string? postalCode)
        {
            var query = _context.Properties
                .Include(p => p.Area)
                .AsQueryable();

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (minSize.HasValue)
                query = query.Where(p => p.Size >= minSize.Value);

            if (!string.IsNullOrEmpty(postalCode))
                query = query.Where(p => p.Area.PostalCode == postalCode);

            var properties = await query
                .OrderByDescending(p => p.ListedDate)
                .Take(50)
                .ToListAsync();

            return _mapper.Map<List<PropertyDto>>(properties);
        }

        public async Task<List<AreaDto>> GetAreasAsync()
        {
            var areas = await _context.Areas
                .Include(a => a.Properties)
                .OrderBy(a => a.Name)
                .ToListAsync();

            return _mapper.Map<List<AreaDto>>(areas);
        }

        public async Task<AreaDto?> GetAreaByPostalCodeAsync(string postalCode)
        {
            var area = await _context.Areas
                .Include(a => a.Properties)
                .FirstOrDefaultAsync(a => a.PostalCode == postalCode);
            
            if (area != null)
            {
                return _mapper.Map<AreaDto>(area);
            }
            return null;
        }

        public async Task<List<PriceHistoryDto>> GetPriceHistoryAsync(string postalCode)
        {
            var history = await _context.PriceHistories
                .Include(p => p.Area)
                .Where(p => p.Area.PostalCode == postalCode)
                .OrderBy(p => p.RecordedAt)
                .Take(50)
                .ToListAsync();
            
            return _mapper.Map<List<PriceHistoryDto>>(history);
        }

        public async Task<List<SavedSearchDto>> GetUserSearchesAsync(int userId)
        {
            var searches = await _context.SavedSearches
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
            
            return _mapper.Map<List<SavedSearchDto>>(searches);
        }
    }
}