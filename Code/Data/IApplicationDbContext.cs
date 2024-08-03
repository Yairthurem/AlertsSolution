using FlightAlerts.Modules;
using Microsoft.EntityFrameworkCore;
public interface IApplicationDbContext
{
    DbSet<Alert> Alerts { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}