using System.Collections.Generic;

namespace TPO_Lab1
{
    public class Shop
    {
        /// <summary>
        /// Общая сумма для покупок
        /// </summary>
        public int MySum { get; set; }

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
            MySum = 1000;
            Products = new Dictionary<string, Product>();
            HowSold = new Dictionary<string, Product>();

            Products.Add("Хлеб", new Product { CoutProduct = 25, Price = 20 });
            Products.Add("Майонез", new Product { CoutProduct = 36, Price = 50 });
            Products.Add("Колбаса", new Product { CoutProduct = 60, Price = 250 });
            Products.Add("Сыр", new Product { CoutProduct = 14, Price = 150 });
            Products.Add("Зубная паста", new Product { CoutProduct = 15, Price = 70 });
            Products.Add("Вино", new Product { CoutProduct = 10, Price = 400 });
            Products.Add("Пиво", new Product { CoutProduct = 30, Price = 100 });
            Products.Add("Шампунь", new Product { CoutProduct = 25, Price = 190 });

            foreach (var item in Products.Keys)
            {
                HowSold.Add(item, new Product { CoutProduct = 0, Price = 0});
            }

        }

        /// <summary>
        /// Покупка товара
        /// </summary>
        /// <param name="name">наименование товара</param>
        /// <param name="count">Количество</param>
        /// <returns>Результат покупки</returns>
        public string Buy(string name, int count)
        {
            if (!Products.ContainsKey(name))
            {
                return "Продукта не существует!";
            }

            var product = Products[name];

            int price = product.Price * count;

            if (price > MySum)
            {
                return "Недостаточно средст для покупки";
            }
            else
            {
                product.CoutProduct -= count;
                Products[name] = product;
                MySum -= price;

                var soldProd = HowSold[name];
                soldProd.CoutProduct += count;
                soldProd.Price += price;
                HowSold[name] = soldProd;

                return "Продано";
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
                Products.Add(name, product);
        }

        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="name">Наименование товара</param>
        public void DeleteProduct(string name)
        {
            if (Products.ContainsKey(name))
                Products.Remove(name);
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
