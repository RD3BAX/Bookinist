using Bookinist.DAL.Entities;
using Bookinist.Services.Interfaces;
using Bookinist.ViewModels;
using Bookinist.Views.Windows;

namespace Bookinist.Services
{
    internal class UserDialogService : IUserDialog
    {
        public bool Edit(Book book)
        {
            var book_editor_model = new BookEditorViewModel(book);

            var book_editor_window = new BookEditorWindow
            {
                DataContext = book_editor_model
            };

            if (book_editor_window.ShowDialog() != true) return false;

            book.Name = book_editor_model.Name;

            return true;
        }
    }
}
