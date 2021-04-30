using System.Collections.Generic;
using System.Threading.Tasks;
using Bookinist.DAL.Entities;

namespace Bookinist.Services.Interfaces
{
    interface ISalesService
    {
        /// <summary>
        /// Отображает все существующие сделки
        /// </summary>
        IEnumerable<Deal> Deals { get; }

        /// <summary>
        /// Совершить сделку.
        /// Принимает параметры сдели кто, что, за какую стоимость и
        /// Формирует новый объект сделка который заносит в БД.
        /// </summary>
        /// <returns></returns>
        Task<Deal> MakeADeal(string BookName, Seller Seller, Buyer Buyer, decimal Price);
    }
}
