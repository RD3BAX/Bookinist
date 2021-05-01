using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        #region Поля

        private readonly IRepository<Book> _books;
        private readonly IRepository<Buyer> _buyers;
        private readonly IRepository<Seller> _sellers;
        private readonly IRepository<Deal> _deals;

        #endregion // Поля

        #region Свойства

        #region BooksCount : int - Количество книг

        /// <summary>Количество книг</summary>
        private int _BooksCount;

        /// <summary>Количество книг</summary>
        public int BooksCount
        {
            get => _BooksCount;
            private set => Set(ref _BooksCount, value);
        }

        #endregion // Количество книг

        #endregion // Свойства

        #region Команды

        #region Command : ComputeStatisticCommand - Вычислить статистические данные

        private ICommand _ComputeStatisticCommand;

        /// <summary>Вычислить статистические данные</summary>
        public ICommand ComputeStatisticCommand => _ComputeStatisticCommand
            ??= new LambdaCommandAsync(OnComputeStatisticCommandExecuted, CanComputeStatisticCommandExecute);

        /// <summary>Проверка возможности выполнения - Вычислить статистические данные</summary>
        private bool CanComputeStatisticCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Вычислить статистические данные</summary>
        private async Task OnComputeStatisticCommandExecuted(object p)
        {
            BooksCount = await _books.Items.CountAsync();

            var deals = _deals.Items;

            var bestsellers = await deals.GroupBy(deal => deal.Book)
                .Select(book_deals => new {Book = book_deals.Key, Count = book_deals.Count()})
                .OrderByDescending(book => book.Count)
                .Take(5)
                .ToArrayAsync();
        }

        #endregion // ComputeStatisticCommand

        #endregion // Команды

        #region Конструктор

        public StatisticViewModel(
            IRepository<Book> books, 
            IRepository<Buyer> buyers, 
            IRepository<Seller> sellers,
            IRepository<Deal> deals)
        {
            _books = books;
            _buyers = buyers;
            _sellers = sellers;
            _deals = deals;
        }

        #endregion // Конструктор
    }
}