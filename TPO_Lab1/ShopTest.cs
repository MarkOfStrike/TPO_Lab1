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
            products.Add("Хлеб", new Product { CoutProduct = 25, Price = 20 });
            products.Add("Майонез", new Product { CoutProduct = 36, Price = 50 });
            products.Add("Колбаса", new Product { CoutProduct = 60, Price = 250 });
            products.Add("Сыр", new Product { CoutProduct = 14, Price = 150 });
            products.Add("Зубная паста", new Product { CoutProduct = 15, Price = 70 });
            products.Add("Вино", new Product { CoutProduct = 10, Price = 400 });
            products.Add("Пиво", new Product { CoutProduct = 30, Price = 100 });
            products.Add("Шампунь", new Product { CoutProduct = 25, Price = 190 });

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
            Assert.AreEqual(-1, shop.Buy("dsadfasdf", 100));//Не существующий товар

            Assert.AreEqual(0, shop.Buy("Майонез", 6473)); //Покупка товара большего количества

            Assert.AreEqual(100, shop.Buy("Майонез", 2));//Покупка и возврат цены

            Assert.AreEqual(34, ((Product)shop.Remainder("Майонез")).CoutProduct); //Остаток продукта

        }

        /// <summary>
        /// Невозможность купить товар
        /// </summary>
        [Test]
        public void TestNotBuy()
        {
            Assert.AreEqual(-1, shop.Buy("yfgshjjab", 2));//Не существующий товар

            Assert.AreEqual(0, shop.Buy("Майонез", 100));//Покупка товара большего количества
        }

        /// <summary>
        /// Привоз товара
        /// </summary>
        [Test]
        public void TestBring()
        {
            Assert.AreEqual(25, ((Product)shop.Remainder("Хлеб")).CoutProduct);//Остаток товара на складе

            Assert.DoesNotThrow(() => { shop.Bringing("Хлеб", 10); });//Привоз товара

            Assert.That(Assert.Throws<Exception>(() => shop.Bringing("23", 1)).Message, Is.EqualTo("Товара не существует"));//Не существующий товар
            Assert.That(Assert.Throws<Exception>(() => shop.Bringing("Хлеб", 0)).Message, Is.EqualTo("Необходимо добавить больше одной еденицы товара"));//Мало товара
            Assert.That(Assert.Throws<ArgumentNullException>(() => shop.Bringing(null, 1)).ParamName, Is.EqualTo("name")); //Вообще не передали ничего

            Assert.AreEqual(35, ((Product)shop.Remainder("Хлеб")).CoutProduct); //Остаток товара на складе

        }

        /// <summary>
        /// Изменение цены товара
        /// </summary>
        [Test]
        public void TestChange()
        {
            Assert.AreEqual(400, ((Product)shop.Remainder("Вино")).Price);


            Assert.DoesNotThrow(() => { shop.ChangePrice("Вино", 500); });

            Assert.That(Assert.Throws<Exception>(() => shop.ChangePrice("lhflkcjnjvj", 500)).Message, Is.EqualTo("Товара не существует"));//Не существующий товар
            Assert.That(Assert.Throws<Exception>(() => shop.ChangePrice("Вино", -1)).Message, Is.EqualTo("Цена не может быть отрицательной"));//Цена ниже 0
            Assert.That(Assert.Throws<ArgumentNullException>(() => shop.ChangePrice("", 500)).ParamName, Is.EqualTo("name")); //Вообще не передали ничего


            Assert.AreEqual(500, ((Product)shop.Remainder("Вино")).Price);


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
            Assert.That(Assert.Throws<ArgumentNullException>(() => { int result = shop.HowManySold(null); }).ParamName, Is.EqualTo("name")); //Вообще не передали ничего

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
            Assert.That(Assert.Throws<ArgumentNullException>(() => { int result = shop.HowMuchSold(null); }).ParamName, Is.EqualTo("name")); //Вообще не передали ничего
        }

        /// <summary>
        /// Добавление нового продукта
        /// </summary>
        [Test]
        public void TestNewProduct()
        {
            Assert.DoesNotThrow(() => shop.NewProduct("Апельсин", new Product { CoutProduct = 50, Price = 20 }));//Добавление товара
            Assert.That(Assert.Throws<Exception>(() => shop.NewProduct("Апельсин", new Product { CoutProduct = 50, Price = 20 })).Message, Is.EqualTo("Товар уже существует"));//Не существующий товар
            Assert.That(Assert.Throws<ArgumentNullException>(() => shop.NewProduct(null, new Product { CoutProduct = 50, Price = 20 })).ParamName, Is.EqualTo("name")); //Вообще не передали ничего
            Assert.That(Assert.Throws<ArgumentNullException>(() => shop.NewProduct("Апельсин", null)).ParamName, Is.EqualTo("product")); //Вообще не передали ничего

            
            Assert.IsTrue(shop.ContainsProduct("Апельсин"));//Проверка на существование товара
            Assert.IsFalse(shop.ContainsProduct("мвап45"));//Проверка на существование товара
            Assert.That(Assert.Throws<ArgumentNullException>(() => shop.ContainsProduct(null)).ParamName, Is.EqualTo("name")); //Вообще не передали ничего
        }

        /// <summary>
        /// Удаление продукта
        /// </summary>
        [Test]
        public void TestDeleteProduct()
        {
            Assert.DoesNotThrow(() => shop.DeleteProduct("Пиво"));//Удаление товара

            Assert.That(Assert.Throws<Exception>(() => shop.DeleteProduct("asdsfrr")).Message, Is.EqualTo("Товар не существует"));//Не существующий товар
            Assert.That(Assert.Throws<ArgumentNullException>(() => shop.DeleteProduct(null)).ParamName, Is.EqualTo("name")); //Вообще не передали ничего

            Assert.IsFalse(shop.ContainsProduct("Пиво"));

        }

    }
}
