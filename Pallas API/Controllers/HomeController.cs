using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pallas_API.Models.Authorization;
using Pallas_API.Services;

namespace Pallas_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        private readonly ILoginService _loginService;
        private readonly IInventoryService _inventoryService;

        public HomeController(ITokenService tokenService,
            IConfiguration configuration, ApplicationDatabaseContext databaseContext)
        {
            _tokenService = tokenService;
            _configuration = configuration;

            _loginService = new LoginService(databaseContext);
            _inventoryService = new InventoryService(databaseContext);
        }

        [HttpGet]
        [Route("inventory-for-metric")]
        [Authorize]
        public async Task<IActionResult> GetInventoryItemsForMetric([FromBody] string metric)
        {
            if (metric == null)
                return BadRequest();

            var inventory = await _inventoryService.GetInventoryItemsForMetricAsync(metric);

            return Ok(inventory);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
        {
            if (credentials.Username == null || credentials.Password == null)
                return BadRequest();

            var exists = await _loginService.ValidateLoginAsync(credentials);

            if (exists == false)
                return Unauthorized();

            var token = _tokenService.BuildToken(_configuration["Jwt:Key"].ToString(),
                            _configuration["Jwt:Issuer"].ToString(), credentials);

            if (token == null)
                return BadRequest();

            HttpContext.Session.SetString("Token", token);
            return new JsonResult(token);
        }
    }
}