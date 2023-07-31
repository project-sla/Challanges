using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Challenges.Persistence.Context;

public class DbContextFactory : IDesignTimeDbContextFactory<ChallengeDbContext>
{
    public ChallengeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ChallengeDbContext>();
        optionsBuilder.UseNpgsql("Host=10.1.23.122;Database=draw;Username=postgres;Password=asd123asd",
            options => options.MigrationsAssembly(typeof(ChallengeDbContext).Assembly.FullName)
        );

        return new ChallengeDbContext(optionsBuilder.Options);
    }
}