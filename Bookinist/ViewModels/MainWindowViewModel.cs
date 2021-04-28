using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Поля

        private readonly IRepository<Book> _booksRepository;

        #endregion // Поля

        #region Свойства

        #region Title : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Title;

        /// <summary>Заголовок</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion // Заголовок

        #endregion // Свойства

        #region Конструктор

        public MainWindowViewModel(IRepository<Book> BooksRepository)
        {
            Title = "Главное окно программы";
            
            _booksRepository = BooksRepository;

            var books = BooksRepository.Items.Take(10).ToArray();
        }

        #endregion // Конструктор
    }
}
