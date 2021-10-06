using System;
using System.Linq;
using System.Collections.Generic;
using StoreBL;
using DL;
using Models;

namespace UI
{
    public class MainMenu : IMenu
    {
        public void Start()
        {
            Console.WriteLine("This is the Main Menu");
            bool exit = false;
            string userInput = "";
            do
            {
                Console.WriteLine("[1] Returning customer.");
                Console.WriteLine("[2] New Customer");
                Console.WriteLine("[x] Exit");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        MenuFactory.GetMenu("current user").Start();
                        break;
                    case "2":
                        // new NewUserMenu(new BL(RAMCustomerRepo.GetInstance())).Start();
                        // new NewUserMenu(new BL(new CustomerFileRepo())).Start();
                        MenuFactory.GetMenu("new user").Start();
                        break;
                    case "3":
                      
                        break;
                    case "x":
                        Console.WriteLine("Exit program.");
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

       
        
    }
}