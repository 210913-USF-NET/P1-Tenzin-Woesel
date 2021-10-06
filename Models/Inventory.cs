namespace Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int ProductID { get; set; }
        public int StoreID { get; set; }

        public Product Product{ get; set; }
        public Inventory() { }

        public Inventory(int Id, int Quantity)
        {
            this.Id = Id;
            this.Quantity = Quantity;
        }

        public override string ToString()
        {
            return $"Inventory Id : {this.Id}\n Quantity: {this.Quantity}";
        }
    }
}