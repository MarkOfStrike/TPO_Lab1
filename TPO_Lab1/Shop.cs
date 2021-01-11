using System;
using System.Collections.Generic;

namespace TPO_Lab1
{
    public class Shop
    {
        /// <summary>
        /// Имеющиеся продукты
        /// </summary>
        private Dictionary<string, Product> _products { get; set; }

        public IReadOnlyDictionary<string, Product> GetAllProducts => _products;

        /// <summary>
        /// Инициализация всех продуктов 
        /// </summary>
        public Shop()
        {
            _products = new Dictionary<string, Product>();
        }

        /// <summary>
        /// Покупка товара
        /// </summary>
        /// <param name="name">наименование товара</param>
        /// <param name="count">Количество</param>
        /// <returns>Результат покупки</returns>
        public int Buy(string name, int count)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Наименование товара не может быть пустым");
            }

            if (!_products.ContainsKey(name))
            {
                throw new Exception("Товар отсутствует");
            }

            if (count <= 0)
            {
                throw new Exception("Такое количество невозможно купить, количество меньше или равно 0");
            }

            var product = _products[name];

            int price = product.Price * count;

            if (product.CountProduct < count)
            {
                throw new Exception("Такого количества товара нет");
            }
            else
            {
                product.CountProduct -= count;
                product.HowMany += count;
                product.HowMuch += price;
                _products[name] = product;

                return price;
            }

        }

        /// <summary>
        /// Привоз товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <param name="count">Количество</param>
        public void Bringing(string name, int count)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Название товара отсутствует");

            if (!_products.ContainsKey(name))
                throw new Exception("Товара не существует");

            if (count < 1)
                throw new Exception("Необходимо добавить больше одной еденицы товара");

            var product = _products[name];
            product.CountProduct += count;
            _products[name] = product;

        }

        /// <summary>
        /// Изменение цены товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <param name="newPrice">Новая цена</param>
        public void ChangePrice(string name, int newPrice)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Название товара отсутствует");

            if (!_products.ContainsKey(name))
                throw new Exception("Товара не существует");

            if (newPrice < 0)
                throw new Exception("Цена не может быть отрицательной");

            var product = _products[name];
            product.Price = newPrice;
            _products[name] = product;
        }

        /// <summary>
        /// Сколько продано товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <returns>Количество проданного товара</returns>
        public int HowManySold(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Название товара отсутствует");

            if (!_products.ContainsKey(name))
                throw new Exception("Товара не существует");

            return _products[name].HowMany;
        }

        /// <summary>
        /// На какую сумму проданно товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <returns>Общая сумма проданной продукции</returns>
        public int HowMuchSold(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Название товара отсутствует");

            if (!_products.ContainsKey(name))
                throw new Exception("Товара не существует");

            return _products[name].HowMuch;
        }

        /// <summary>
        /// Добавление нового продукта
        /// </summary>
        /// <param name="name">Наименование продукта</param>
        /// <param name="product">Продукт</param>
        public void NewProduct(string name, Product product)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Название товара отсутствует");

            if (_products.ContainsKey(name))
                throw new Exception("Товар уже существует");

            _products.Add(name, product);
        }

        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        public void DeleteProduct(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Название товара отсутствует");

            if (!_products.ContainsKey(name))
                throw new Exception("Товар не существует");

            _products.Remove(name);

        }

        /// <summary>
        /// Остаток товара на складе
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <returns>Количество оставшегося товара</returns>
        public int GetRemainder(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Название товара отсутствует");

            if (!_products.ContainsKey(name))
                throw new Exception("Товар не существует");

            return _products[name].CountProduct;
        }

        public int GetPriceProduct(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Название товара отсутствует");

            if (!_products.ContainsKey(name))
                throw new Exception("Товар не существует");

            return _products[name].Price;
        }

        /// <summary>
        /// Проверка на существование товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <returns>существует ли товар</returns>
        public bool ContainsProduct(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Название товара отсутствует");

            return _products.ContainsKey(name);
        }

        /// <summary>
        /// Вся информация о товаре
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public (string Name, Product Product) GetProduct(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Название товара отсутствует");

            if (!_products.ContainsKey(name))
                throw new Exception("Товар не существует");

            return (name, _products[name]);
        }
    }

    public struct Product
    {
        public int CountProduct;
        public int Price;
        public int HowMany;
        public int HowMuch;

    }
}
