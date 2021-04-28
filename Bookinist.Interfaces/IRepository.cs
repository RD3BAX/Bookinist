using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bookinist.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        /// <summary>
        /// Получение доступа ко всему что хранится в репозитории
        /// </summary>
        IQueryable<T> Items { get; }

        /// <summary>
        /// Возвращает сущность по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        Task<T> GetAsync(int id, CancellationToken Cancel = default);

        /// <summary>
        /// Добавляет сущность в репозиторий
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Возвращает добавленную сущность</returns>
        T Add(T item);
        Task<T> AddAsync(T item, CancellationToken Cancel = default);

        void Update(T item);
        Task UpdateAsync(T item, CancellationToken Cancel = default);

        void Remove(int id);
        Task RemoveAsync(int id, CancellationToken Cancel = default);
    }
}