using AutoMapper;
using HackerApiConnector.Domain.Interfaces.RestServices;
using HackerApiConnector.Domain.Interfaces.Services;
using HackerApiConnector.Domain.Models.Models;
using HackerApiConnector.Domain.Models.ViewModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace HackerApiConnector.Application.Services
{
    public class ConnectorService : IConnectorService
    {
        private readonly IHackerApiRequestService _hackerApiRequestService;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;


        public ConnectorService(IHackerApiRequestService hackerApiRequestService, IMapper mapper, IDistributedCache cache)
        {
            _hackerApiRequestService = hackerApiRequestService;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<List<BeststorieDetailedViewModel>> GetStoriesDetailed(int n)
        {
            var result = new List<BeststorieByIdModel>();

            string cacheKey = $"beststories_{n}";
            var cached = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cached))
                result = JsonSerializer.Deserialize<List<BeststorieByIdModel>>(cached);
            else
            {
                var response = await _hackerApiRequestService.GetBestStories();
                var filtredResponse = response.response.Take(n);

                foreach (var item in filtredResponse)
                    result.Add(await _hackerApiRequestService.GetBestStoriesById(item));

                result = result.OrderByDescending(x => x.score).ToList();
                await SetRedis(result, cacheKey);

            }

            return _mapper.Map<List<BeststorieDetailedViewModel>>(result);
        }

        private async Task SetRedis(List<BeststorieByIdModel>? result, string cacheKey)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(result), options);
        }
    }
}
