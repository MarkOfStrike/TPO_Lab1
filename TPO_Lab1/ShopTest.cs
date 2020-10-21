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


        [Test]
        public void TestBuy()
        {
            Assert.AreEqual(100, shop.Buy("Майонез", 2));
            Assert.AreEqual(34, shop.Remainder("Майонез").CoutProduct);

        }

        [Test]
        public void TestNotBuy()
        {
            Assert.AreEqual(-1, shop.Buy("yfgshjjab", 2));
            Assert.AreEqual(0, shop.Buy("Майонез", 100));
        }


        [Test]
        public void TestBring()
        {
            Assert.AreEqual(25, shop.Remainder("Хлеб").CoutProduct);
            Assert.DoesNotThrow(() => { shop.Bringing("Хлеб", 10); });
            Assert.AreEqual(35, shop.Remainder("Хлеб").CoutProduct);

        }

        [Test]
        public void TestChange()
        {
            Assert.AreEqual(400, shop.Remainder("Вино").Price);
            Assert.DoesNotThrow(() => { shop.ChangePrice("Вино", 500); });
            Assert.AreEqual(500, shop.Remainder("Вино").Price);

        }

        [Test]
        public void TestHowMany()
        {
            shop.Buy("Майонез", 2);
            Assert.AreEqual(2, shop.HowManySold("Майонез"));
        }

        [Test]
        public void TestHowMuch()
        {
            shop.Buy("Майонез", 2);
            Assert.AreEqual(100, shop.HowMuchSold("Майонез"));
        }

        [Test]
        public void TestNewProduct()
        {
            shop.NewProduct("Апельсин", new Product { CoutProduct = 50, Price = 20 });
            Assert.IsTrue(shop.ContainsProduct("Апельсин"));

        }

        [Test]
        public void TestDeleteProduct()
        {
            shop.DeleteProduct("Пиво");
            Assert.IsFalse(shop.ContainsProduct("Пиво"));

        }

    }
}
