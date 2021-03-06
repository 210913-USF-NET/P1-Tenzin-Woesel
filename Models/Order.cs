using System;
using System.Collections.Generic;
using System.Net.Security;
using Microsoft.VisualBasic;

namespace Models
{
    public class Order
    {
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

        public Order(int customerId, int storeId)
        {
            this.CustomerId = customerId;
            this.StoreFrontId = storeId;
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
            return $"Order Id: {this.Id}\nTotal Price: {this.Total}\nDate Ordered: {this.OrderDate}";
        }

    }
}