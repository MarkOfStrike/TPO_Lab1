using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace TPO_Lab1
{
    [TestFixture]
    class ShopTest
    {
        Shop shop;

        [SetUp]
        public void InitTest()
        {
            shop = new Shop();
            Dictionary<string, Product> products = new Dictionary<string, Product>();
            products.Add("Хлеб", new Product { CountProduct = 25, Price = 20 });
            products.Add("Майонез", new Product { CountProduct = 36, Price = 50 });
            products.Add("Колбаса", new Product { CountProduct = 60, Price = 250 });
            products.Add("Сыр", new Product { CountProduct = 14, Price = 150 });
            products.Add("Зубная паста", new Product { CountProduct = 15, Price = 70 });
            products.Add("Вино", new Product { CountProduct = 10, Price = 400 });
            products.Add("Пиво", new Product { CountProduct = 30, Price = 100 });
            products.Add("Шампунь", new Product { CountProduct = 25, Price = 190 });

            foreach (var item in products)
            {
                shop.NewProduct(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Покупка товара
        /// </summary>
        [Test]
        public void TestBuy()
        {

            Assert.AreEqual(100, shop.Buy("Майонез", 2));//Покупка и возврат цены
            Assert.AreEqual(34, shop.GetRemainder("Майонез")); //Остаток продукта

        }

        /// <summary>
        /// Невозможность купить товар
        /// </summary>
        [Test]
        public void TestNotBuy()
        {
            Assert.That(Assert.Throws<Exception>(() => shop.Buy("Майонез", -2)).Message, Is.EqualTo("Такое количество невозможно купить, количество меньше или равно 0"));
            Assert.That(Assert.Throws<Exception>(() => shop.Buy("Майонез", 0)).Message, Is.EqualTo("Такое количество невозможно купить, количество меньше или равно 0"));

            Assert.That(Assert.Throws<Exception>(() => shop.Buy("dsadfasdf", 100)).Message, Is.EqualTo("Товар отсутствует"));//Не существующий товар
            Assert.That(Assert.Throws<Exception>(() => shop.Buy("Майонез", 6473)).Message, Is.EqualTo("Такого количества товара нет"));//Мало товара
            Assert.That(Assert.Throws<Exception>(() => shop.Buy("", 100)).Message, Is.EqualTo("Наименование товара не может быть пустым")); //Вообще не передали ничего

        }

        /// <summary>
        /// Привоз товара
        /// </summary>
        [Test]
        public void TestBring()
        {
            Assert.AreEqual(25, shop.GetRemainder("Хлеб"));//Остаток товара на складе

            Assert.DoesNotThrow(() => { shop.Bringing("Хлеб", 10); });//Привоз товара

            Assert.That(Assert.Throws<Exception>(() => shop.Bringing("23", 1)).Message, Is.EqualTo("Товара не существует"));//Не существующий товар
            Assert.That(Assert.Throws<Exception>(() => shop.Bringing("Хлеб", 0)).Message, Is.EqualTo("Необходимо добавить больше одной еденицы товара"));//Мало товара
            Assert.That(Assert.Throws<Exception>(() => shop.Bringing("Хлеб", -1)).Message, Is.EqualTo("Необходимо добавить больше одной еденицы товара"));//Мало товара
            Assert.That(Assert.Throws<Exception>(() => shop.Bringing(null, 1)).Message, Is.EqualTo("Название товара отсутствует")); //Вообще не передали ничего

            Assert.AreEqual(35, shop.GetRemainder("Хлеб")); //Остаток товара на складе

        }

        /// <summary>
        /// Изменение цены товара
        /// </summary>
        [Test]
        public void TestChange()
        {
            Assert.AreEqual(400, shop.GetPriceProduct("Вино"));


            Assert.DoesNotThrow(() => { shop.ChangePrice("Вино", 500); });

            Assert.That(Assert.Throws<Exception>(() => shop.ChangePrice("lhflkcjnjvj", 500)).Message, Is.EqualTo("Товара не существует"));//Не существующий товар
            Assert.That(Assert.Throws<Exception>(() => shop.ChangePrice("Вино", -1)).Message, Is.EqualTo("Цена не может быть отрицательной"));//Цена ниже 0
            Assert.That(Assert.Throws<Exception>(() => shop.ChangePrice("", 500)).Message, Is.EqualTo("Название товара отсутствует")); //Вообще не передали ничего


            Assert.AreEqual(500, shop.GetPriceProduct("Вино"));

            Assert.DoesNotThrow(() => { shop.ChangePrice("Вино", 0); });
            Assert.AreEqual(0, shop.GetPriceProduct("Вино"));
        }

        /// <summary>
        /// Сколько продано товара
        /// </summary>
        [Test]
        public void TestHowMany()
        {
            shop.Buy("Майонез", 2);

            Assert.AreEqual(2, shop.HowManySold("Майонез"));
            Assert.That(Assert.Throws<Exception>(() => { int result = shop.HowManySold("3ger"); }).Message, Is.EqualTo("Товара не существует"));//Не существующий товар
            Assert.That(Assert.Throws<Exception>(() => { int result = shop.HowManySold(null); }).Message, Is.EqualTo("Название товара отсутствует")); //Вообще не передали ничего

        }

        /// <summary>
        /// На сколько продали товара
        /// </summary>
        [Test]
        public void TestHowMuch()
        {
            shop.Buy("Майонез", 2);
            Assert.AreEqual(100, shop.HowMuchSold("Майонез"));
            Assert.That(Assert.Throws<Exception>(() => { int result = shop.HowMuchSold("ffs33"); }).Message, Is.EqualTo("Товара не существует"));//Не существующий товар
            Assert.That(Assert.Throws<Exception>(() => { int result = shop.HowMuchSold(null); }).Message, Is.EqualTo("Название товара отсутствует")); //Вообще не передали ничего
        }

        /// <summary>
        /// Добавление нового продукта
        /// </summary>
        [Test]
        public void TestNewProduct()
        {
            Assert.DoesNotThrow(() => shop.NewProduct("Апельсин", new Product { CountProduct = 50, Price = 20 }));//Добавление товара
            Assert.That(Assert.Throws<Exception>(() => shop.NewProduct("Апельсин", new Product { CountProduct = 50, Price = 20 })).Message, Is.EqualTo("Товар уже существует"));//Не существующий товар
            Assert.That(Assert.Throws<Exception>(() => shop.NewProduct(null, new Product { CountProduct = 50, Price = 20 })).Message, Is.EqualTo("Название товара отсутствует")); //Вообще не передали ничего

            
            Assert.IsTrue(shop.ContainsProduct("Апельсин"));//Проверка на существование товара
            Assert.IsFalse(shop.ContainsProduct("мвап45"));//Проверка на существование товара
            Assert.That(Assert.Throws<Exception>(() => shop.ContainsProduct(null)).Message, Is.EqualTo("Название товара отсутствует")); //Вообще не передали ничего
        }

        /// <summary>
        /// Удаление продукта
        /// </summary>
        [Test]
        public void TestDeleteProduct()
        {
            Assert.DoesNotThrow(() => shop.DeleteProduct("Пиво"));//Удаление товара

            Assert.That(Assert.Throws<Exception>(() => shop.DeleteProduct("asdsfrr")).Message, Is.EqualTo("Товар не существует"));//Не существующий товар
            Assert.That(Assert.Throws<Exception>(() => shop.DeleteProduct(null)).Message, Is.EqualTo("Название товара отсутствует")); //Вообще не передали ничего

            Assert.IsFalse(shop.ContainsProduct("Пиво"));

        }

        [Test]
        public void TestAllInformation()
        {
            (string Name, Product Product) info = ("Сыр", new Product { CountProduct = 14, Price = 150, HowMany = 0, HowMuch = 0 });

            var actual = shop.GetProduct("Сыр");

            Assert.AreEqual(info, actual);

            Assert.That(Assert.Throws<Exception>(() => { var i = shop.GetProduct("asdsfrr"); }).Message, Is.EqualTo("Товар не существует"));//Не существующий товар
            Assert.That(Assert.Throws<Exception>(() => { var i = shop.GetProduct(null); }).Message, Is.EqualTo("Название товара отсутствует")); //Вообще не передали ничего
        }

        public void TestCountProduct()
        {
            Assert.AreEqual(8, shop.GetAllProducts.Count);

            shop.NewProduct("Абрикос", new Product { CountProduct = 80, Price = 35 });

            Assert.AreEqual(9, shop.GetAllProducts.Count);

        }
    }
}
