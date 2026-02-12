using HackerApiConnector.Domain.Models.Models;

namespace HackerApiConnector.Domain.Interfaces.RestServices
{
    public interface IHackerApiRequestService
    {
        Task<BeststoriesModel> GetBestStories();
        Task<BeststorieByIdModel> GetBestStoriesById(int id);
    }
}
