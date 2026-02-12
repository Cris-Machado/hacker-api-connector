using HackerApiConnector.Domain.Models.ViewModels;

namespace HackerApiConnector.Domain.Interfaces.Services
{
    public interface IConnectorService
    {
        public Task<List<BeststorieDetailedViewModel>> GetStoriesDetailed(int n);
    }
}
