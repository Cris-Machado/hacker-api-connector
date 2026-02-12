using HackerApiConnector.Domain.Models.ResponseModels;
using Refit;

namespace HackerApiConnector.Infrastructure.RestService.HttpClient
{
    public interface IHackerApiHttpClient
    {
        [Get("/beststories.json")]
        Task<ApiResponse<int[]>> GetBestStories();
        
        [Get("/item/{id}.json")]
        Task<ApiResponse<BeststorieByIdResponseModel>> GetBestStorieById(int id);
    }
}
