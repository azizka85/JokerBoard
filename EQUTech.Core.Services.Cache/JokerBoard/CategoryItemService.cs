using EQUTech.Core.Grpc.Models.JokerBoard;
using EQUTech.Core.Services.JokerBoard;
using Microsoft.Extensions.Caching.Memory;

namespace EQUTech.Core.Services.Cache.JokerBoard;

public sealed class CategoryItemService : ICategoryItemService
{
    private readonly IMemoryCache _memoryCache;

    private readonly ICategoryItemService _categoryItemService;

    private readonly string _cacheKey;
    private readonly TimeSpan _cacheLifeTime;

    public CategoryItemService(IMemoryCache memoryCache, ICategoryItemService categoryItemService, string cacheKey, TimeSpan cacheLifeTime)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

        _categoryItemService = categoryItemService ?? throw new ArgumentNullException(nameof(categoryItemService));

        _cacheKey = cacheKey ?? throw new ArgumentNullException(nameof(cacheKey));
        _cacheLifeTime = cacheLifeTime;
    }

    public CategoryItems List()
    {
        if (_memoryCache.TryGetValue(_cacheKey, out var cachedValue))
        {
            return (CategoryItems)cachedValue!;
        }

        var data = _categoryItemService.List();

        _memoryCache.Set(_cacheKey, data, _cacheLifeTime);

        return data;
    }
}
