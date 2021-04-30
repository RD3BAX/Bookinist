using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        #region Поля

        private readonly IRepository<Book> _BooksRepository;

        #endregion // Поля

        #region Конструктор

        public BooksViewModel(IRepository<Book> BooksRepository)
        {
            _BooksRepository = BooksRepository;
        }

        #endregion // Конструктор
    }
}