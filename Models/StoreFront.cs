using System.Collections.Generic;

namespace Models
{
    public class StoreFront
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Inventory> Inventories { get; set; }

        public List<Order> Orders { get; set; }
        public StoreFront() { }

        public StoreFront(int id) : this()
        {
            this.Id = id;
        }
        public StoreFront(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }
        /*
            The store front contains information pertaining the various store locations.
                Properties:
                    • Name
                    • Address
                    • Inventory
                    • List of Orders

        */

        public override string ToString()
        {
            return $"Store Name: {this.Name} \nAddress: {this.Address} \n";
        }
    }
}