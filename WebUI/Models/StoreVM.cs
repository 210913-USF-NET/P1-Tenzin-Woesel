using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WebUI.Models
{
    public class StoreVM
    {

        public StoreVM() { }
        //public StoreVM() { }

        public StoreVM(StoreFront store)
        {
            this.Id = store.Id;
            this.Name = store.Name;
            this.Address = store.Address;
            this.Inventories = store.Inventories;
        }
        public int Id { get; set; }
        [Display(Name = "Store Name")]
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public List<Inventory> Inventories { get; set; }

        public StoreFront ToModel()
        {
            return new StoreFront
            {
                Id = this.Id,
                Name = this.Name,
                Address = this.Address
            };
        }

    }
}
