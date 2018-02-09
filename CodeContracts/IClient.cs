#define CONTRACTS_FULL

using System;
using System.Diagnostics.Contracts;

namespace CodeContracts
{
    /// <summary>
    /// Базовый интерфейс клиента.
    /// </summary>
    [ContractClass(typeof(ClientContract))] // Задаем имя класса, в котором будут описаны контракты.
    public interface IClient
    {
        /// <summary>
        /// Имя клиента.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Количество денег клиента.
        /// </summary>
        decimal Money { get; }

        /// <summary>
        /// Купить товар.
        /// </summary>
        /// <param name="productName"> Название товара. </param>
        /// <param name="price"> Цена товара. </param>
        /// <returns> Сообщение о покупке. </returns>
        string Bay(string productName, decimal price);
    }

    [ContractClassFor(typeof(IClient))] // Указываем к какому интерфейсу относится класс контрактов.
    internal abstract class ClientContract : IClient
    {
        /// <summary>
        /// Контракт для свойства Имя.
        /// Имя не может быть пустым или равным null.
        /// </summary>
        public string Name
        {
            get
            {
                Contract.Requires<ArgumentNullException>
                (
                    !string.IsNullOrWhiteSpace(Name),
                    $"Имя клиента в свойстве {nameof(Name)} имеет значение null или пустое."
                );

                return string.Empty;
            }
        }

        /// <summary>
        /// Контракт для свойства Деньги.
        /// Количество денег должно быть больше нуля. 
        /// </summary>
        public decimal Money
        {
            get
            {
                Contract.Requires<ArgumentException>
                (
                    Money >= 0,
                    $"Количество денег клиента {Name} в свойстве {nameof(Money)} имеет значение ({Money}) меньше либо равное нулю."
                );

                return decimal.One;
            }
        }

        /// <summary>
        /// Контракт на метод покупки.
        /// </summary>
        /// <param name="productName"> Название продукта не должно быть пустым. </param>
        /// <param name="price"> 
        /// Стоимость не может быть ниже либо равна нулю. 
        /// Стоимость не может превышать количество денег у клиента.
        /// </param>
        /// <returns> Возвращаемое сообщение не может быть пустым. </returns>
        public string Bay(string productName, decimal price)
        {
            Contract.Requires<ArgumentNullException>
            (
                !string.IsNullOrWhiteSpace(productName), 
                $"Передаваемое название товара в методе {nameof(Bay)} имеет значение null или пустое."
            );

            Contract.Requires<ArgumentException>
            (
                price > 0,
                $"Стоимость товара {productName} в методе {nameof(Bay)} имеет значение ({price}) меньше либо равное нулю."
            );

            Contract.Requires<ArgumentException>
            (
                price <= Money,
                $"Стоимость товара {productName} в методе {nameof(Bay)} имеет значение ({price}) большее чем количество денег ({Money}) у клиента {Name}."
            );

            Contract.Invariant(Money >= 0);

            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            return string.Empty;
        }
    }
}
