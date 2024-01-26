using CqrsWithEfCoreAndMediaR.Data;
using Microsoft.EntityFrameworkCore;

namespace CqrsWithEfCoreAndMediaR.Services;

public class DbContextFactory
{
    public static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=Database.db")
            .Options;

        AppDbContext dbContext = new(options);

        return dbContext;
    }
}
