using System.Collections.Generic;
using Serilog;

namespace Models
{
    public class Product
    {
        private string _name;
        public int Id { get; set; }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length == 0)
                {
                    InputInvalidException e = new InputInvalidException("Product Name should not be empty");
                    Log.Warning(e.Message);
                    throw e;
                }
                else
                {
                    _name = value;
                }
            }
        }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public List<Inventory> Inventories { get; set; }

        public List<LineItems> LineItems { get; set; }

        public Product() { }
        public Product(string name, decimal price, string description, string category)
        {
            this.Name = name;
            this.Price = price;
            this.Description = description;
            this.Category = category;

        }
        /*
            The Product model is supposed to hold the data concerning a customer.
                Properties:
                    • Name
                    • Price
                    • Desc. (optional)
                    • Category (optional)
        */

        public override string ToString()
        {
            return $"Product Id: {this.Id}\nProduct Name: {this.Name}\nProduct Price: {this.Price}\nProduct Description: {this.Description}\n";
        }
    }
}