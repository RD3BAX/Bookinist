using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class BuyersViewModel : ViewModel
    {
        #region Поля

        private readonly IRepository<Buyer> _Buyers;

        #endregion // Поля

        #region Конструктор

        public BuyersViewModel(IRepository<Buyer> Buyers)
        {
            _Buyers = Buyers;
        }

        #endregion // Конструктор
    }
}
