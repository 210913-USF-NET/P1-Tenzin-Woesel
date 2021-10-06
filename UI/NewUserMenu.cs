using System;
using System.Collections.Generic;
using StoreBL;
using Models;
using Serilog;

namespace UI
{
    public class NewUserMenu : IMenu
    {
        private IBL _bl;
        public NewUserMenu(IBL bl)
        {
            _bl = bl;

        }
        public void Start()
        {

            Console.WriteLine("Welcome to SLS");
            bool exit = false;
            string userInput = "";
            do
            {
                Console.WriteLine("[1] Create Account");
                Console.WriteLine("[2] Cancel");
                Console.WriteLine("[x] Go to Main Menu");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        CreateUser();
                        break;
                    case "2":
                        exit = true;
                        break;
                    case "x":
                        Console.WriteLine("Go to main menu.");
                        exit = true;
                        break;
                    case "admin":
                        MenuFactory.GetMenu("admin").Start();
                        break;
                    default:
                        Console.WriteLine("Ivalid input.");
                        break;
                }
            } while (!exit);
        }

        private void CreateUser()
        {
            JustBorder();
            Console.WriteLine("Creating User\n");
            Customer cust = new Customer();
        inputName:
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            List<Customer> allCustomers = _bl.SearchCustomer(name);
            //if name is already in DB then promt user to give another name
            if (allCustomers == null || allCustomers.Count == 0)
            {
                cust.Name = name;
            }
            else
            {
                Console.WriteLine("Name in use already. Please enter another name.");
                goto inputName;
            }

            Console.WriteLine("\nEnter Address: ");
            string address = Console.ReadLine();
            cust.Address = address;

        inputEmail:
            Console.WriteLine("\nEnter Email Address :");
            string email = Console.ReadLine();
            try
            {
                cust.Email = email;
            }
            catch (InputInvalidException e)
            {
                Console.WriteLine(e.Message);
                goto inputEmail;
            }

            Customer addedCustomer = _bl.AddCustomer(cust);
            JustBorder();
            Console.WriteLine($"\nYou created {addedCustomer}");
            Log.Information("Customer created successfully.");
            JustBorder();

            MenuFactory.GetMenu("current user").Start();
        }
        private void GetACustomer()
        {

        }

        private void JustBorder()
        {
            Console.WriteLine("==========================\n");
        }
    }
}