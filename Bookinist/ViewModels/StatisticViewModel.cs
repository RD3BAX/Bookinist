using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        #region Поля

        private readonly IRepository<Book> _books;
        private readonly IRepository<Buyer> _buyers;
        private readonly IRepository<Seller> _sellers;

        #endregion // Поля

        #region Конструктор

        public StatisticViewModel(IRepository<Book> books, IRepository<Buyer> buyers, IRepository<Seller> sellers)
        {
            _books = books;
            _buyers = buyers;
            _sellers = sellers;
        }

        #endregion // Конструктор
    }
}