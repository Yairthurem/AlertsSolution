using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightAlerts.Data;
using FlightAlerts.Modules;

namespace FlightAlerts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<AlertsController> _logger;

        public AlertsController(IApplicationDbContext context, ILogger<AlertsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alert>>> GetAlerts()
        {
            _logger.LogInformation("GetAlerts - Fetching all alerts.");
            return await _context.Alerts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alert>> GetAlert(Guid id)
        {
            var alert = await _context.Alerts.FindAsync(id);

            if (alert == null)
            {
                _logger.LogWarning($"GetAlert - Alert with ID {id} not found.");
                return NotFound();
            }

            return alert;
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<IEnumerable<Alert>>> GetAlertsByUserName(string username)
        {
            var alerts = await _context.Alerts
                                       .Where(a => a.UserName == username)
                                       .ToListAsync();

            if (alerts == null || !alerts.Any())
            {
                _logger.LogWarning($"GetAlertsByUserName - No alerts found for user {username}.");
                return NotFound();
            }

            return alerts;
        }

        [HttpPost]
        public async Task<ActionResult<Alert>> PostAlert(Alert alert)
        {
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlert), new { id = alert.AlertId }, alert);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlert(Guid id, Alert alert)
        {
            if (id != alert.AlertId)
            {
                _logger.LogWarning($"PutAlert - ID mismatch: {id} != {alert.AlertId}.");
                return NotFound();
            }

            _context.Alerts.Update(alert);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertExists(id))
                {
                    _logger.LogWarning($"PutAlert - Alert with ID {id} not found.");
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"PutAlert - Error updating alert with ID {id}.");
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(Guid id)
        {
            var alert = await _context.Alerts.FindAsync(id);
            if (alert == null)
            {
                _logger.LogWarning($"DeleteAlert - Alert with ID {id} not found.");
                return NotFound();
            }

            _context.Alerts.Remove(alert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlertExists(Guid id)
        {
            return _context.Alerts.Any(e => e.AlertId == id);
        }
    }
}
