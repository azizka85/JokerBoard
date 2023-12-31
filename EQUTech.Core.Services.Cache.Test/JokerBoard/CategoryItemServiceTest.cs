﻿using EQUTech.Core.Services.Cache.JokerBoard;
using Microsoft.Extensions.Caching.Memory;
using CategoryItemServiceMock = EQUTech.Core.Mocks.JokerBoard.CategoryItemService;

namespace EQUTech.Core.Services.Cache.Test.JokerBoard;

[TestClass]
public class CategoryItemServiceTest
{
    [TestMethod]
    public void List()
    {
        var memoryCache = new MemoryCache
        (
            new MemoryCacheOptions()
        );

        var service = new CategoryItemServiceMock();

        var cacheService = new CategoryItemService
        (
            memoryCache,
            service,
            "cache",
            TimeSpan.FromMinutes(10)
        );

        var list = service.List();
        var result = cacheService.List();

        Assert.IsNotNull(result);

        Assert.AreEqual(list.List.Count, result.List.Count);
        Assert.AreEqual(list.List[0].Id, result.List[0].Id);
        Assert.AreEqual(list.List[0].Name, result.List[0].Name);

    }
}
