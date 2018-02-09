#define CONTRACTS_FULL

using System;
using System.Diagnostics.Contracts;

namespace CodeContracts
{
    /// <summary>
    /// Клиент.
    /// </summary>
    public class Client : IClient
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Деньги.
        /// </summary>
        public decimal Money { get; private set; }

        /// <summary>
        /// Создать экземпляр клиента.
        /// </summary>
        /// <param name="name"> Имя. </param>
        /// <param name="money"> Деньги. </param>
        public Client(string name, decimal money)
        {
            Contract.Requires<ArgumentNullException>
            (
                !string.IsNullOrWhiteSpace(name),
                $"Имя клиента ({nameof(name)}) в конструкторе класса {nameof(Client)} имеет значение null или пустое."
            );

            Contract.Requires<ArgumentException>
            (
                money >= 0,
                $"Количество денег в свойстве {nameof(money)} в конструкторе класса {nameof(Client)} имеет значение ({money}) меньше нуля."
            );

            Name = name;
            Money = money;
        }

        /// <summary>
        /// Купить товар.
        /// </summary>
        /// <param name="productName"> Название товара. </param>
        /// <param name="price"> Стоимость товара. </param>
        /// <returns> Сообщение о покупке. </returns>
        public string Bay(string productName, decimal price)
        {
            Money -= price;
            return $"Client {Name} bought {productName} at a price {price}$ {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}";
        }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Имя. </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
