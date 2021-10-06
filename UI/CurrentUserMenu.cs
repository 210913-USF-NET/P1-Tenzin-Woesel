using System;
using System.Collections.Generic;
using StoreBL;
using Models;
using Serilog;

namespace UI
{
    public class CurrentUserMenu : IMenu
    {
        private IBL _bl;

        public CurrentUserMenu(IBL bl)
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
                Console.WriteLine("[1] Log In");
                Console.WriteLine("[2] Cancel");
                Console.WriteLine("[x] Go to Main Menu");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        ValidateUser();
                        break;
                    case "3":
                        getAStore();
                        break;
                    case "2":
                        exit = true;
                        break;
                    case "x":
                        Console.WriteLine("Go to main menu.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Ivalid input.");
                        break;
                }
            } while (!exit);
        }

        private void ValidateUser()
        {
        enterName:
            Console.WriteLine("Enter your name");
            string cName = Console.ReadLine();
            List<Customer> customers = _bl.SearchCustomer(cName);

            if (customers == null && customers.Equals(""))
            {
                Console.WriteLine("No such users :/");
                goto enterName;
            }
            else
            {
                

                Customer realCustomer = _bl.GetCustomer(cName);

                if (realCustomer == null)
                {
                    Console.WriteLine("No user by that name. Please enter your user name or create an account.");
                    return;
                }

                Console.WriteLine("Welcome to your profile : " + realCustomer.Name);

                StaticService.currentCustomer = realCustomer;

                MenuFactory.GetMenu("welcome menu").Start();
            }
        }

        private void getAStore()
        {
            System.Console.WriteLine(_bl.SelectStore(1));
        }
    }
}