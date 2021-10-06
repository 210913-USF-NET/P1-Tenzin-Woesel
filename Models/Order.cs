using System;
using System.Collections.Generic;
using System.Net.Security;
using Microsoft.VisualBasic;

namespace Models
{
    public class Order
    {
        // public Order(){}
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int StoreFrontId { get; set; }

        public DateTime OrderDate { get; set; }
        public List<LineItems> LineItems { get; set; }

        public decimal Total { get; set; }
        public Order(decimal total) : this()
        {
            this.Total = total;
        }

        public Order()
        {
            this.LineItems = new List<LineItems>();
        }
        /*
            The orders contain information about customer orders.
                Properties:
                    • Order Line Items
                    • Location (that the order was placed)
                    • Total price
        */

        public override string ToString()
        {
            // string temp = $"Total Price: {this.Total}\n";
            // foreach (LineItems item in LineItems)
            // {
            //     temp += $" Quantity : {item.Quantity}";
            // }
            // return temp;
            return $"Order Id: {this.Id}\nTotal Price: {this.Total}\nDate Ordered: {this.OrderDate}";
        }

    }
}