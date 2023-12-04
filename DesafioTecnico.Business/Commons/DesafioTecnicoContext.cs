using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DesafioTecnico.Business.Commons
{
    internal class DesafioTecnicoContext : DbContext
    {
        private static readonly object _lock = new();
        private static bool _migrationsApplied = false;
        private readonly static IList<object?> _maps = BuscarMapeamentos();

        public DesafioTecnicoContext(DbContextOptions<DesafioTecnicoContext> options) : base(options)
        {
            ApplyMigrates();
        }

        protected DesafioTecnicoContext()
        {
            ApplyMigrates();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterMaps(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void ApplyMigrates()
        {
            lock (_lock)
            {
                if (_migrationsApplied)
                    return;

                _migrationsApplied = true;

                if (!this.AllMigrationApplied())
                    Database.Migrate();
            }
        }
        private static IList<object?> BuscarMapeamentos()
        {
            var baseMap = typeof(EntityMapBase<>);
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(w => !w.IsInterface && !w.IsAbstract && w.BaseType != null && w.BaseType.IsGenericType && w.BaseType.GetGenericTypeDefinition() == baseMap)
                .Select(s => Activator.CreateInstance(s))
                .ToList();
        }
        private static void RegisterMaps(ModelBuilder modelBuilder)
        {
            var method = modelBuilder
                .GetType()
                .GetMethod("ApplyConfiguration");

            if (method == null)
                throw new Exception("Não foi possível localizar o ponto de entrada do método 'ApplyConfiguration'.");

            foreach (var map in _maps)
            {
                if (map == null)
                    continue;

                var mapType = map
                    .GetType()
                    .GetInterfaces()
                    .FirstOrDefault(f => f.IsGenericType && f.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

                if (mapType == null)
                    continue;

                method
                    .MakeGenericMethod(mapType.GenericTypeArguments[0])
                    .Invoke(modelBuilder, new object[] { map });
            }
        }

    }
}