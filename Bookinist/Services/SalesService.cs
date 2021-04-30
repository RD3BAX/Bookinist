using System.Collections.Generic;
using System.Threading.Tasks;
using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookinist.Services
{
    class SalesService : ISalesService
    {
        #region Поля

        private readonly IRepository<Book> _books;
        private readonly IRepository<Deal> _deals;

        #endregion // Поля

        #region Свойства

        /// <summary>
        /// Отображает все существующие сделки
        /// </summary>
        public IEnumerable<Deal> Deals => _deals.Items;

        #endregion // Свойства

        #region Методы

        /// <summary>
        /// Совершить сделку.
        /// Принимает параметры сдели кто, что, за какую стоимость и
        /// Формирует новый объект сделка который заносит в БД.
        /// </summary>
        /// <returns></returns>
        public async Task<Deal> MakeADeal(string BookName, Seller Seller, Buyer Buyer, decimal Price)
        {
            var book = await _books.Items.FirstOrDefaultAsync(b => b.Name == BookName).ConfigureAwait(false);
            if (book is null) return null;

            var deal = new Deal
            {
                Book = book,
                Seller = Seller,
                Buyer = Buyer,
                Price = Price
            };

            return await _deals.AddAsync(deal);
        }

        #endregion // Методы

        #region Конструктор

        public SalesService(
            IRepository<Book> Books, 
            IRepository<Deal> Deals)
        {
            _books = Books;
            _deals = Deals;
        }

        #endregion // Конструктор
    }
}
