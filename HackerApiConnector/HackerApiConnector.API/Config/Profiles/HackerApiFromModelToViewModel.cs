using AutoMapper;
using HackerApiConnector.Domain.Models.Models;
using HackerApiConnector.Domain.Models.ViewModels;

namespace HackerApiConnector.API.Config.Profiles
{
    public class HackerApiFromModelToViewModel : Profile
    {
        public HackerApiFromModelToViewModel()
        {
            CreateMap<BeststorieByIdModel, BeststorieDetailedViewModel>()
                .ForMember(dest => dest.Uri, org => org.MapFrom(src => src.url))
                .ForMember(dest => dest.PostedBy, org => org.MapFrom(src => src.by))
                .ForMember(dest => dest.CommentCount, org => org.MapFrom(src => src.descendants))
                .ReverseMap();
        }
    }
}
