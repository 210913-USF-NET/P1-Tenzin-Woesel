namespace Models
{
    public class LineItems
    {
        /*
            The line items contain information about a particular product and its quantity.
                Properties:
                    • Product
                    • Quantity
        */
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public override string ToString()
        {
            return $"Quantity: {this.Quantity}\nProduct: {this.ProductId}";
        }

    }
}