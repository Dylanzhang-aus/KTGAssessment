using System.Collections.Generic;
using KTG.Functions;
using KTG.Models;
using Xunit;

//Author: HANYUAN ZHANG, 2021
namespace UnitTest
{
    public class MinOverCancellationTest
    {
        private readonly MinOverCancellation minOverCancellation = new();

        //Test data
        private readonly Order order1 = new() { Id = 1, Quantity = 3, Price = 100 };
        private readonly Order order2 = new() { Id = 2, Quantity = 2, Price = 100 };
        private readonly Order order3 = new() { Id = 3, Quantity = 3, Price = 100 };
        private readonly Order order4 = new() { Id = 4, Quantity = 1, Price = 100 };
        private readonly Order order5 = new() { Id = 5, Quantity = 5, Price = 100 };
        private readonly Order order6 = new() { Id = 1, Quantity = 5, Price = 100 };
        private readonly Order order7 = new() { Id = 3, Quantity = 7, Price = 100 };
        private readonly Order order8 = new() { Id = 4, Quantity = 2, Price = 100 };
        private readonly Order order9 = new() { Id = 2, Quantity = 5, Price = 100 };
        private readonly Order order10 = new() { Id = 3, Quantity = 5, Price = 100 };
        private readonly Order order11 = new() { Id = 4, Quantity = 6, Price = 100 };
        private readonly Order order12 = new() { Id = 5, Quantity = 15, Price = 100 };
        private readonly Order order13 = new() { Id = 6, Quantity = 55, Price = 100 };



       //Example 1
       [Fact]
       public void TestCase1()
       {
            List<Order> orders = new() { order1, order2, order3, order4, order5 };

            var actual = minOverCancellation.Collect(orders, 2);
            var expected = new List<int> { 2 };

            Assert.Equal(expected, actual);           
       }


        //Example 2
        [Fact]
        public void TestCase2()
        {
            List<Order> orders = new() { order1, order2, order3, order4, order5 };

            var actual = minOverCancellation.Collect(orders, 7);
            var expected = new List<int> { 2 , 5};

            Assert.Equal(expected, actual);
        }


        //Example 3
        [Fact]
        public void TestCase3()
        {
            List<Order> orders = new() { order6, order2, order7, order8};

            var actual = minOverCancellation.Collect(orders, 3);
            var expected = new List<int> { 2, 4 };

            Assert.Equal(expected, actual);
        }


        //Example 4
        [Fact]
        public void TestCase4()
        {
            List<Order> orders = new() { order6, order9, order10, order11, order12, order13 };

            var actual = minOverCancellation.Collect(orders, 12);
            var expected = new List<int> { 5 };

            Assert.Equal(expected, actual);
        }


        //Added testCase
        //If the cancellation quantity is greater than the existing order quantity,
        //it should return empty.
        [Fact]
        public void TestCase5()
        {
            List<Order> orders = new() { order2 };

            var actual = minOverCancellation.Collect(orders, 5);

            Assert.Null(actual);
        }
    } 
}
