using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Serilog;

namespace Models
{
    public class Customer
    {
        public Customer()
        {
            this.Orders = new List<Order>();
        }
        public Customer(string name, string address, string email)
        {
            this.Name = name;
            this.Address = address;
            this.Email = email;

        }
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9 !?']+$", ErrorMessage = "Customer name can only have alphanumeric characters, ! and ?.")]
        public string Name { get; set; }

        public string Address { get; set; }

        public List<Order> Orders { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Customer ID: {this.Id}\nCustomer Name: {this.Name} \nAddress: {this.Address} \nEmail:{this.Email} \n";
        }

        public bool Equals(Customer customer)
        {
            return this.Name == customer.Name && this.Address == customer.Address && this.Email == customer.Email;
        }
    }
}
