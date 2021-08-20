using System;
using System.Collections.Generic;
using System.Linq;
using KTG.Models;

//Author: HANYUAN ZHANG, 2021
namespace KTG.Functions
{
    public class InternalTransfer
    {

        public List<int> Collect(List<Order> orders)
        {
            List<int> results = new();

            //collect all possible combination in "orders", store inside "tempBox".
            var TotolCount = (int)Math.Pow(2, orders.Count) - 1;

            //A temporary box to store all possible combinations
            List<List<Order>> tempBox = new();

            for (int i = 1; i <= TotolCount + 1; i++)
            {
                //The combination like : [order1, order2] or [order3, order6, order9] etc..
                List<Order> combination = new();

                //combining
                for (int j = 0; j < orders.Count; j++)
                {
                    if ((i >> j) % 2 != 0)
                    {
                        combination.Add(orders[j]);
                    }
                }

                //Grab combinations which the sum is zero and the size of combinations is not one.
                //"conbination.Count>1" is to filter out orders with a zero order quantity,
                //although this seems unlikely to happen.
                if (combination.Count > 1 && combination.Sum(o => o.Quantity) == 0)
                {
                    var flag = combination.First().Price;
                    if(combination.All(o => o.Price == flag))
                    {
                        tempBox.Add(combination);
                    }
                }
            }

            //If no suitable combination is found in the collection of Orders, the empty list is returned
            if (tempBox.Count == 0)
            {
                return results;
            }
            else
            {
                //Arrange all possible combinations in descending order, taking the one with the largest size
                var orderedTempBox = tempBox.OrderByDescending(o => o.Count).ToList();

                foreach (var order in orderedTempBox[0])
                {
                    results.Add(order.Id);
                }

                return results;
            }

        }
    }
}
