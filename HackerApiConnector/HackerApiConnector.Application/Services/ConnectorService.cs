using AutoMapper;
using HackerApiConnector.Domain.Exceptions;
using HackerApiConnector.Domain.Interfaces.RestServices;
using HackerApiConnector.Domain.Interfaces.Services;
using HackerApiConnector.Domain.Models.Models;
using HackerApiConnector.Domain.Models.ViewModels;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace HackerApiConnector.Application.Services
{
    public class ConnectorService : IConnectorService
    {
        private readonly IHackerApiRequestService _hackerApiRequestService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;


        public ConnectorService(IHackerApiRequestService hackerApiRequestService, IMapper mapper, IMemoryCache cache)
        {
            _hackerApiRequestService = hackerApiRequestService;
            _mapper = mapper;
            _cache = cache;
        }



        public async Task<List<BeststorieDetailedViewModel>> GetStoriesDetailed(int n)
        {

            var response = await GetBestStoriesCached();
            if(n > response.Count())
                n = response.Count();

            var filtredResponse = response.Take(n);
            var tasks = filtredResponse.Select(item => GetBestStoryByIdCached(item));
            var result = await Task.WhenAll(tasks);

            var orderedResult = result.OrderByDescending(x => x.score).ToList();
            return _mapper.Map<List<BeststorieDetailedViewModel>>(orderedResult);

        }
        public async Task<BeststorieByIdModel> GetBestStoryByIdCached(int id)
        {
            if (!_cache.TryGetValue($"Story_{id}", out BeststorieByIdModel story))
            {
                story = await _hackerApiRequestService.GetBestStoriesById(id);

                _cache.Set($"Story_{id}", story, TimeSpan.FromMinutes(300));
            }

            return story;
        }


        public async Task<List<int>> GetBestStoriesCached()
        {
            if (!_cache.TryGetValue("BestStories", out List<int> stories))
            {
                var response = await _hackerApiRequestService.GetBestStories();
                stories = response.response.ToList();

                _cache.Set("BestStories", stories, TimeSpan.FromMinutes(300));
            }

            return stories;
        }



    }
}
