using AuthSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MigrationsController : Controller
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MigrationsController> _logger;

        public MigrationsController(ILogger<MigrationsController> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEntityFrameworkMigration()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                _logger.LogInformation("Starting Entity Framework Migration...");
                var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
                await context.Database.MigrateAsync();
                _logger.LogInformation("Migration DONE!");
            }

            return NoContent();
        }
    }
}