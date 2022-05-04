using DAL.Context;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace DAL;

public class AppDbInitializer
{
    private readonly AppDb _context;
    private readonly ILogger _logger;

    public AppDbInitializer(AppDb context, ILogger<AppDbInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitializeAsync (bool removeBefore = false, CancellationToken cancellationToken = default)
    {
        if (removeBefore)
            await RemoveAsync();
        await _context.Database.MigrateAsync(cancellationToken);
        _logger.LogInformation("DB Created");

        await AddTestDataAsync(cancellationToken);
    }

    private async Task AddTestDataAsync(CancellationToken cancellationToken)
    {
        if(! await _context.Users.AnyAsync(cancellationToken))
        {
            var users = Enumerable
                .Range(1, 5)
                .Select(i => new User
                {
                    Name = $"Firstname0{i} Lastname0{i}",
                    Email = $"someEmail000{i}@notonline.ru",
                    BirthDay = DateTime.Now.AddYears(-18 -2*i)
                });
            foreach (var user in users)
            {
                await _context.Users.AddAsync(user,cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Users added");
        }
    }

    private async Task RemoveAsync(CancellationToken cancellationToken = default)
    {
        await _context.Database.EnsureDeletedAsync(cancellationToken).ConfigureAwait(false);
    }
}
