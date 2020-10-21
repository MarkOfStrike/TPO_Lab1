using System.Collections.Generic;

namespace TPO_Lab1
{
    public class Shop
    {
        /// <summary>
        /// Имеющиеся продукты
        /// </summary>
        private Dictionary<string, Product> Products { get; set; }

        /// <summary>
        /// Отчет по продажам
        /// </summary>
        private Dictionary<string, Product> HowSold { get; set; }

        /// <summary>
        /// Инициализация всех продуктов 
        /// </summary>
        public Shop()
        {
            Products = new Dictionary<string, Product>();
            HowSold = new Dictionary<string, Product>();
        }

        /// <summary>
        /// Покупка товара
        /// </summary>
        /// <param name="name">наименование товара</param>
        /// <param name="count">Количество</param>
        /// <returns>Результат покупки</returns>
        public int Buy(string name, int count)
        {
            if (!Products.ContainsKey(name))
            {
                return -1;
            }

            var product = Products[name];

            int price = product.Price * count;

            if (product.CoutProduct < count)
            {
                return 0;
            }
            else
            {
                product.CoutProduct -= count;
                Products[name] = product;

                var soldProd = HowSold[name];
                soldProd.CoutProduct += count;
                soldProd.Price += price;
                HowSold[name] = soldProd;

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
            var product = Products[name];
            product.CoutProduct += count;
            Products[name] = product;
        }

        /// <summary>
        /// Изменение цены товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <param name="newPrice">Новая цена</param>
        public void ChangePrice(string name, int newPrice)
        {
            var product = Products[name];
            product.Price = newPrice;
            Products[name] = product;
        }

        /// <summary>
        /// Сколько продано товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <returns>Количество проданного товара</returns>
        public int HowManySold(string name)
        {
            return HowSold[name].CoutProduct;
        }

        /// <summary>
        /// На какую сумму проданно товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <returns>Общая сумма проданной продукции</returns>
        public int HowMuchSold(string name)
        {
            return HowSold[name].Price;
        }

        /// <summary>
        /// Добавление нового продукта
        /// </summary>
        /// <param name="name">Наименование продукта</param>
        /// <param name="product">Продукт</param>
        public void NewProduct(string name, Product product)
        {
            if (!Products.ContainsKey(name))
            {
                Products.Add(name, product);
                HowSold.Add(name, new Product {CoutProduct = 0, Price=0 });
            }
                
        }

        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        public void DeleteProduct(string name)
        {
            if (Products.ContainsKey(name))
            {
                Products.Remove(name);
                HowSold.Remove(name);
            }
                
        }

        /// <summary>
        /// Остаток товара на складе
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <returns>Количество оставшегося товара</returns>
        public Product Remainder(string name)
        {
            return Products[name];
        }

        /// <summary>
        /// Проверка на существование товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        /// <returns>существует ли товар</returns>
        public bool ContainsProduct(string name)
        {
            return Products.ContainsKey(name);
        }

    }

    /// <summary>
    /// Продукт
    /// </summary>
    public struct Product
    {
        public int CoutProduct;
        public int Price;

    }
}
