using BoligRadar.API.DTO;

namespace BoligRadar.API.Services;

public interface IPropertyService
{
    Task<List<PropertyDto>> GetPropertiesAsync(int? minPrice, int? maxPrice, int? minSize, string? postalCode);
    Task<List<AreaDto>> GetAreasAsync();
    Task<AreaDto?> GetAreaByPostalCodeAsync(string postalCode);
    Task<List<PriceHistoryDto>> GetPriceHistoryAsync(string postalCode);
    Task<SavedSearchDto> CreateSavedSearchAsync(int userId, CreateSearchDto createSearchDto);
    Task<List<SavedSearchDto>> GetUserSearchesAsync(int userId);
}