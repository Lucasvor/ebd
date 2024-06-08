using Ebd.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ebd.Presentation.Tests.Integration.Setup.Context;

public class TestDbContext : MainContext
{
    public TestDbContext(DataBaseConfiguration configuration, IConfiguration appConfiguration) : base(configuration, appConfiguration)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbName = $"dbtest-{Guid.NewGuid()}";

        var connString = appConfiguration.GetConnectionString("SqlServer");

        connString = connString + $"database={dbName}";

        optionsBuilder.UseSqlServer(connString);
    }
}
