using HackerApiConnector.Domain.Interfaces.Services;
using HackerApiConnector.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace HackerApiConnector.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectorController : ControllerBase
    {
        private readonly ILogger<ConnectorController> _logger;
        private readonly IConnectorService _connectorService;

        public ConnectorController(ILogger<ConnectorController> logger, IConnectorService connectorService)
        {
            _logger = logger;
            _connectorService = connectorService;
        }

        [HttpGet("get-stories-detailed/{n}")]
        [ProducesResponseType(typeof(IEnumerable<BeststorieDetailedViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]

        public async Task<IActionResult> Get(int n)
        {
            var result = await _connectorService.GetStoriesDetailed(n);
            if (result == null)
                return NoContent();

            return Ok(result);
        }
    }
}
