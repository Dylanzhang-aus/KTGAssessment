using System;
using System.Collections.Generic;
using System.Linq;
using KTG.Models;

//Author: HANYUAN ZHANG, 2021
namespace KTG.Functions
{
    public class MinOverCancellation
    {
        public List<int> Collect(List<Order> orders, int cancelTarget)
        {

            //collect all possible combination in "orders", store inside "tempBox".
            var TotolCount = (int)Math.Pow(2, orders.Count) - 1;
            List<List<Order>> tempBox = new();
            Dictionary<List<int>, int> tempResults = new();

            for (int i = 1; i <= TotolCount + 1; i++)
            {
                tempBox.Add(new List<Order>());

                for (int j = 0; j < orders.Count; j++)
                {
                    if ((i >> j) % 2 != 0)
                    {
                        tempBox.Last().Add(orders[j]);
                    }
                }
            }

            //Calculates the set of the number of orders in all combinations set,
            //excluding those whose total number is less than the cancelTarget.
            foreach (var orderList in tempBox)
            {
                List<int> idList = new();

                foreach (var order in orderList)
                {
                    idList.Add(order.Id);
                }

                if (orderList.Sum(o => o.Quantity) >= cancelTarget)
                {
                    tempResults.Add(idList, orderList.Sum(o => o.Quantity));
                }
            }

            //Find the number closest to canceltarge in the current set and record its index in the dictionary
            var minValue = int.MaxValue;
            int minIndex = -1;
            int index = -1;

            foreach (var aa in tempResults)
            {
                index++;
                if (aa.Value - cancelTarget <= minValue)
                {
                    minValue = aa.Value - cancelTarget;
                    minIndex = index;
                }
            }

            //Return the results.
            if (index == -1)
            {
                return null;
            }
            else
            {
                return tempResults.ElementAt(minIndex).Key;
            }
        }
    }
}
