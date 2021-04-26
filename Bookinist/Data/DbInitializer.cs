using Bookinist.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bookinist.Data
{
    class DbInitializer
    {
        private readonly BookinistDB _db;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(BookinistDB db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void Initialize()
        {
            // Удаляет базу данных (нужен при начальной отладке)
            _db.Database.EnsureDeleted();

            //_db.Database.EnsureCreated();

            // Позволяет создать базу данных если ее нет, а также накатить изменения на уже существующую
            _db.Database.Migrate();
        }
    }
}
