using AutoMapper;
using HackerApiConnector.API.Config;
using HackerApiConnector.Application.Services;
using HackerApiConnector.Domain.Interfaces.RestServices;
using HackerApiConnector.Domain.Interfaces.Services;
using HackerApiConnector.Domain.Models.Models;
using NSubstitute;
using Ploeh.AutoFixture;
using Xunit;

namespace HackerApiConnector.Tests.Services
{
    public class ConnectorServiceTest
    {
        private readonly IConnectorService _connector;
        private readonly IHackerApiRequestService _hackerApiRequestService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        public ConnectorServiceTest()
        {
            _fixture = new Fixture();
            _hackerApiRequestService = Substitute.For<IHackerApiRequestService>();
            _mapper = AutoMapperTestConfigurator.CreateMapper();
            _connector = new ConnectorService(_hackerApiRequestService, _mapper);
        }

        [Fact]
        public void GetBestStoriesWithSuccess()
        {
            var storedReturn = _fixture.Build<BeststoriesModel>().CreateMany(1);
            _hackerApiRequestService.GetBestStories().Returns(storedReturn.ToList()[0]);

            var result = _hackerApiRequestService.GetBestStories();
            Assert.NotNull(result);
        }
    }
}
