using System.Collections.Generic;
using KTG.Functions;
using KTG.Models;
using Xunit;

//Author: HANYUAN ZHANG, 2021
namespace UnitTest
{
    public class InternalTransferTest
    {
        private readonly InternalTransfer internalTransfer = new();

        //Test data
        private readonly Order order1 = new() { Id = 1, Quantity = 10, Price = 100 };
        private readonly Order order2 = new() { Id = 2, Quantity = -5, Price = 100 };
        private readonly Order order3 = new() { Id = 3, Quantity = -5, Price = 100 };
        private readonly Order order4 = new() { Id = 4, Quantity = 5, Price = 100 };
        private readonly Order order5 = new() { Id = 5, Quantity = -5, Price = 100 };
        private readonly Order order6 = new() { Id = 6, Quantity = 2, Price = 100 };
        private readonly Order order7 = new() { Id = 2, Quantity = -3, Price = 100 };
        private readonly Order order8 = new() { Id = 5, Quantity = -2, Price = 100 };
        private readonly Order order9 = new() { Id = 2, Quantity = -10, Price = 200 };
        private readonly Order order10 = new() { Id = 7, Quantity = 10, Price = 200 };


        //Example 1
        [Fact]
        public void TestCase1()
        {
            List<Order> orders = new() { order1, order2, order3, order4, order5, order6 };

            var actual = internalTransfer.Collect(orders);
            var expected = new List<int> { 1, 2, 3, 4, 5 };

            Assert.Equal(expected, actual);
        }


        //Example 2
        [Fact]
        public void TestCase2()
        {
            List<Order> orders = new() { order1, order2 };

            var actual = internalTransfer.Collect(orders);

            Assert.True( actual.Count == 0);
        }


        //Example 3
        [Fact]
        public void TestCase3()
        {
            List<Order> orders = new() { order1, order7, order3, order4, order8 };

            var actual = internalTransfer.Collect(orders);
            var expected = new List<int> { 1, 2, 3, 5 };

            Assert.Equal(expected, actual);
        }


        //Example 4
        [Fact]
        public void TestCase4()
        {
            List<Order> orders = new() { order1, order9 };

            var actual = internalTransfer.Collect(orders);

            Assert.True(actual.Count == 0);
        }


        //Added testCase
        [Fact]
        public void TestCase5()
        {
            List<Order> orders = new() { order2, order3, order4, order10 };

            var actual = internalTransfer.Collect(orders);
            var expected = new List<int> { 4, 2 };

            Assert.Equal(expected, actual);
        }
    }
}
