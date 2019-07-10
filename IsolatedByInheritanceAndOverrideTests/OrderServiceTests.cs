using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.Extensions;

namespace IsolatedByInheritanceAndOverride.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        /// <summary>
        /// Tests the synchronize book orders 3 orders only 2 book order.
        /// ProductName, Type, Price, CustomerName
        /// 商品1,        Book,  100, Kyo
        /// 商品2,        DVD,   200, Kyo
        /// 商品3,        Book,  300, Joey
        /// </summary>
        [TestMethod()]
        public void Test_SyncBookOrders_3_Orders_Only_2_book_order()
        {
            //Try to isolate dependency to unit test

            //var target = new OrderService();
            //target.SyncBookOrders();
            //verify bookDao.Insert() twice
            var list = new List<Order>();
            list.Add(new Order()
            {
                ProductName = "商品1",
                Type = "Book",
                Price = 100,
                CustomerName = "Kyo"
            });
            list.Add(new Order()
            {
                ProductName = "商品2",
                Type = "DVD",
                Price = 200,
                CustomerName = "Kyo"
            });
            list.Add(new Order()
            {
                ProductName = "商品3",
                Type = "Book",
                Price = 300,
                CustomerName = "Joey"
            });

            var FakeOrderService = Substitute.For<OrderService>();
            var FakeBookDao = Substitute.For<BookDao>();
            FakeBookDao.When(x => x.Insert(Arg.Any<Order>())).Do(y => { });
            FakeOrderService.Get().ReturnsForAnyArgs(list);
            FakeOrderService.GetBookDao().ReturnsForAnyArgs(FakeBookDao);

            FakeOrderService.SyncBookOrders();

            FakeBookDao.Received(2).Insert(Arg.Any<Order>());
        }
    }
}