using AutoMapper;
using HackerApiConnector.Domain.Models.Models;
using HackerApiConnector.Domain.Models.ResponseModels;

namespace HackerApiConnector.API.Config.Profiles
{
    public class HackerApiFromResponseToModel : Profile
    {
        public HackerApiFromResponseToModel()
        {
            CreateMap<BeststoriesResponseModel, BeststoriesModel>().ReverseMap();
            CreateMap<BeststorieByIdResponseModel, BeststorieByIdModel>().ReverseMap();
        }
    }
}
