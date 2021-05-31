using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bookinist.DAL.Context;
using Bookinist.DAL.Entities.Base;
using Bookinist.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.DAL
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        #region Поля

        private readonly BookinistDB _db;
        private readonly DbSet<T> _Set;

        #endregion // Поля

        #region Свойства

        /// <summary>
        /// Управляет автоматическим сохранением в контексте,
        /// вызывать автоматически метод SaveChanges или нет
        /// </summary>
        public bool AutoSaveChanges { get; set; }

        public virtual IQueryable<T> Items => _Set;

        #endregion // Свойства

        #region Методы

        public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default) => await Items
            .SingleOrDefaultAsync(item => item.Id == id, Cancel)
            .ConfigureAwait(false);

        public T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                _db.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }

        public void Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public void Remove(int id)
        {
            //var item = Get(id);
            //if (item is null) return;
            //_db.Entry(item).State = EntityState.Deleted;

            // Если сущность есть в кеше то она будет передана на удаление
            var item = _Set.Local.FirstOrDefault(i => i.Id == id) ?? new T {Id = id};

            _db.Remove(item);

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        #endregion // Методы

        #region Конструктор

        public DbRepository(BookinistDB db)
        {
            _db = db;
            _Set = db.Set<T>();
            AutoSaveChanges = true;
        }

        #endregion // Конструктор
    }
}
