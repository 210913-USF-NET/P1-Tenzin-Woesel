using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class LoginVM
    {
        public LoginVM() { }

        public LoginVM(Customer customer)
        {
            this.Id = customer.Id;
            this.Name = customer.Name;
            this.Email = customer.Email;
            this.Address = customer.Address;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public Customer ToModel()
        {
            return new Customer
            {
                Id = this.Id,
                Name = this.Name,
                Email = this.Email,
                Address = this.Address
            };
        }
    }
}




