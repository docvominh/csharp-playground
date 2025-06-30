using EntityFrameworkMigration.Database;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkMigration.Test;

public class DbContextFactory
{
    public static AppDbContext CreateDbContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;

        AppDbContext context = new (options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.SaveChanges();

        return context;
    }
}