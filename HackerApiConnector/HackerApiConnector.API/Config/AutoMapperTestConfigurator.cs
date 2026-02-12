using AutoMapper;
using HackerApiConnector.API.Config.Profiles;

namespace HackerApiConnector.API.Config
{
    public static class AutoMapperTestConfigurator
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<HackerApiFromModelToViewModel>();
                cfg.AddProfile<HackerApiFromResponseToModel>();
            }, null);

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }

}
