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
        }


        [Test]
        public void TestBuy()
        {
            Assert.AreEqual(1000, shop.MySum);
            Assert.AreEqual("Продано", shop.Buy("Майонез", 2));
            Assert.AreEqual(34, shop.Remainder("Майонез").CoutProduct);
            Assert.AreEqual(900, shop.MySum);

        }

        [Test]
        public void TestNotBuy()
        {
            Assert.AreEqual("Продукта не существует!", shop.Buy("yfgshjjab", 2));
            Assert.AreEqual("Недостаточно средст для покупки", shop.Buy("Майонез", 100));
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
            shop.NewProduct("Апельсин", new Product { CoutProduct = 50, Price = 20});
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
