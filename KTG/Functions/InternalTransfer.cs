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
            List<Order> tempBox = new();
            List<Order> sellingOrders = new();
            List<Order> buyingOrders = new();

            //Separate buying orders and selling orders
            foreach (var order in orders)
            {
                if (order.Quantity > 0)
                {
                    buyingOrders.Add(order);
                }
                else if (order.Quantity < 0)
                {
                    sellingOrders.Add(order);
                }
            }

            //The quantity of the order is extracted from the collection of buying order,
            //and offsetting is realized with the quantity of the order in selling order.
            foreach (var buyingOrder in buyingOrders)
            {

                foreach (var sellingOrder in sellingOrders)
                {
                    if (tempBox.Count >= 1)
                    {
                        if (buyingOrder.Price == sellingOrder.Price)
                        {
                            tempBox.Add(sellingOrder);

                            if (tempBox.Sum(o => o.Quantity) + buyingOrder.Quantity == 0)
                            {
                                results.Add(buyingOrder.Id);

                                foreach (var tempOrder in tempBox)
                                {
                                    results.Add(tempOrder.Id);
                                    sellingOrders.Remove(tempOrder);
                                }
                                tempBox.Clear();
                                break;
                            }
                        }
                    }
                    else
                    {
                        //If there is only one Selling order in the temporary box
                        if (buyingOrder.Price == sellingOrder.Price)
                        {
                            tempBox.Add(sellingOrder);

                            if (tempBox[0].Quantity + buyingOrder.Quantity == 0)
                            {
                                results.Add(buyingOrder.Id);
                                results.Add(tempBox[0].Id);
                                sellingOrders.Remove(tempBox[0]);
                                tempBox.Clear();
                                break;
                            }
                        }
                    }
                }
            }

            return results;
        }
    }
}
