using AutoMapper;
using HackerApiConnector.Domain.Exceptions;
using HackerApiConnector.Domain.Interfaces.RestServices;
using HackerApiConnector.Domain.Models.Models;
using HackerApiConnector.Domain.Models.ResponseModels;
using HackerApiConnector.Infrastructure.RestService.HttpClient;
using Microsoft.Extensions.Caching.Distributed;
using Refit;
using System.Net;
using System.Text.Json;

namespace HackerApiConnector.Infrastructure.RestService.Services
{
    public class HackerApiRequestService : IHackerApiRequestService
    {
        private readonly IHackerApiHttpClient _hackerApiHttpClient;
        private readonly IMapper _mapper;
        public HackerApiRequestService(IHackerApiHttpClient hackerApiHttpClient, IMapper mapper)
        {
            _hackerApiHttpClient = hackerApiHttpClient;
            _mapper = mapper;
        }

        #region public methods
        public async Task<BeststoriesModel> GetBestStories()
        {
            var result = await _hackerApiHttpClient.GetBestStories();
            VerifyResult(result);

            var mappedModel = new BeststoriesResponseModel { response = result.Content.ToList() };
            return _mapper.Map<BeststoriesModel>(mappedModel);
        }

        public async Task<BeststorieByIdModel> GetBestStoriesById(int id)
        {
            var result = await _hackerApiHttpClient.GetBestStorieById(id);
            VerifyResult(result);
            return _mapper.Map<BeststorieByIdModel>(result.Content);
        }
        #endregion

        #region private methods
        private static void VerifyResult<TEntity>(ApiResponse<TEntity> result)
        {
            if (!result.IsSuccessStatusCode)
                throw new BadGatewayException();
            else if (result.StatusCode == HttpStatusCode.NoContent)
                throw new NoContentException();
        }
        #endregion
    }
}
