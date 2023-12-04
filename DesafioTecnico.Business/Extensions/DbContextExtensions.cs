using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.EntityFrameworkCore
{
    internal static class DbContextExtensions
    {
        public static bool AllMigrationApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);
            var total = context.GetService<IMigrationsAssembly>()
                .Migrations.Select(m => m.Key);
            return !total.Except(applied).Any();
        }
    }
}