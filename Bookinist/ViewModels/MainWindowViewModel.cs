using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
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

        public MainWindowViewModel()
        {
            Title = "Главное окно программы";
        }

        #endregion // Конструктор
    }
}
