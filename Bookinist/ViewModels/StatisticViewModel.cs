using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Models;
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

        public ObservableCollection<BestSellerInfo> BestSellers { get; } = new ObservableCollection<BestSellerInfo>();

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
            await ComputeDealsStatisticAsync();
            
            //var bestsellers = await bestsellers_query.ToDictionaryAsync(
            //    deal => deal.Book, deal => deal.Count);
        }

        private async Task ComputeDealsStatisticAsync()
        {
            var bestsellers_query = _deals.Items
                .GroupBy(b => b.Book.Id)
                .Select(deals => new
                {
                    BookId = deals.Key, Count = deals.Count(), Sum = deals.Sum(d => d.Price)
                })
                .OrderByDescending(deals => deals.Count)
                .Take(5)
                .Join(_books.Items,
                    deals => deals.BookId,
                    book => book.Id,
                    (deals, book) => new BestSellerInfo
                    {
                        Book = book, 
                        SellCount = deals.Count, 
                        SumCost = deals.Sum
                    });

            //BestSellers.Clear();
            BestSellers.AddClear(await bestsellers_query.ToArrayAsync());

            //foreach ( var bestseller in await bestsellers_query.ToArrayAsync())
            //    BestSellers.Add(bestseller);
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