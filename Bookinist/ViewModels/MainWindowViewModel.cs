using System.Windows.Input;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Поля

        private readonly IUserDialog _userDialog;
        private readonly IRepository<Book> _books;
        private readonly IRepository<Seller> _sellers;
        private readonly IRepository<Buyer> _buyers;
        private readonly IRepository<Deal> _deals;
        private readonly ISalesService _salesService;

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

        #region CurrentModel : ViewModel - Текущая дочерняя модель-представления

        /// <summary>Текущая дочерняя модель-представления</summary>
        private ViewModel _CurrentModel;

        /// <summary>Текущая дочерняя модель-представления</summary>
        public ViewModel CurrentModel
        {
            get => _CurrentModel;
            private set => Set(ref _CurrentModel, value);
        }

        #endregion // Текущая дочерняя модель-представления

        #endregion // Свойства

        #region Команды

        #region Command : ShowBooksViewCommand - Отобразить представление книг

        private ICommand _ShowBooksViewCommand;

        /// <summary>Отобразить представление книг</summary>
        public ICommand ShowBooksViewCommand => _ShowBooksViewCommand
            ??= new LambdaCommand(OnShowBooksViewCommandExecuted, CanShowBooksViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление книг</summary>
        private bool CanShowBooksViewCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Отобразить представление книг</summary>
        private void OnShowBooksViewCommandExecuted(object p)
        {
            CurrentModel = new BooksViewModel(_books, _userDialog);
        }

        #endregion // ShowBooksViewCommand

        #region Command : ShowBuyersViewCommand - Отобразить представление покупателей

        private ICommand _ShowBuyersViewCommand;

        /// <summary>Отобразить представление покупателей</summary>
        public ICommand ShowBuyersViewCommand => _ShowBuyersViewCommand
            ??= new LambdaCommand(OnShowBuyersViewCommandExecuted, CanShowBuyersViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление покупателей</summary>
        private bool CanShowBuyersViewCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Отобразить представление покупателей</summary>
        private void OnShowBuyersViewCommandExecuted(object p)
        {
            CurrentModel = new BuyersViewModel(_buyers);
        }

        #endregion // ShowBuyersViewCommand

        #region Command : ShowStatisticViewCommand - Отобразить представление статистики

        private ICommand _ShowStatisticViewCommand;

        /// <summary>Отобразить представление статистики</summary>
        public ICommand ShowStatisticViewCommand => _ShowStatisticViewCommand
            ??= new LambdaCommand(OnShowStatisticViewCommandExecuted, CanShowStatisticViewCommandExecute);

        /// <summary>Проверка возможности выполнения - Отобразить представление статистики</summary>
        private bool CanShowStatisticViewCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Отобразить представление статистики</summary>
        private void OnShowStatisticViewCommandExecuted(object p)
        {
            CurrentModel = new StatisticViewModel(
                _books, _buyers, _sellers,
                _deals
                );
        }

        #endregion // ShowStatisticViewCommand

        #endregion // Команды

        #region Методы

        //private async void Test()
        //{
        //    var deals_count = _salesService.Deals.Count();

        //    var book = await _booksRepository.GetAsync(5);
        //    var buyer = await _buyers.GetAsync(3);
        //    var seller = await _sellers.GetAsync(7);

        //    var deal = _salesService.MakeADeal(book.Name, seller, buyer, 100m);

        //    var deals_count2 = _salesService.Deals.Count();
        //}

        #endregion // Методы

        #region Конструктор

        public MainWindowViewModel(
            IUserDialog UserDialog,
            IRepository<Book> Books, 
            IRepository<Seller> Sellers,
            IRepository<Buyer> Buyers,
            IRepository<Deal> Deals,
            ISalesService SalesService)
        {
            Title = "Главное окно программы";

            _userDialog = UserDialog;
            _books = Books;
            _sellers = Sellers;
            _buyers = Buyers;
            _deals = Deals;
            _salesService = SalesService;
        }

        #endregion // Конструктор
    }
}
