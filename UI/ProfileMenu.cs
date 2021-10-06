using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Models;
using StoreBL;

namespace UI
{
    public class ProfileMenu : IMenu
    {
        private IBL _bl;

        private StoreService _storeService;

        public ProfileMenu(IBL bl, StoreService storeService)
        {
            _bl = bl;
            _storeService = storeService;
        }
        public void Start()
        {
            bool goBack = false;
            string userInput = "";
            do
            {
                Console.WriteLine("Welcome to you profile.");
                Console.WriteLine("[1] View Order History");
                Console.WriteLine("[2] View Details of the account.");
                Console.WriteLine("[3] Go back.");
                Console.WriteLine("[x] Main Menu");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        OrderHistory();
                        break;
                    case "2":
                        GetDetails();
                        break;
                    case "3":
                        goBack = true;
                        break;
                    case "admin":
                        MenuFactory.GetMenu("admin").Start();
                        break;
                    case "x":
                        goBack = true;
                        break;

                    default:
                        Console.WriteLine("Invalid Input.");
                        break;
                }
            } while (!goBack);
        }

        private void OrderHistory()
        {
            List<Order> orders = _bl.GetAllOrders();
            List<LineItems> lineItems = _bl.GetLineItems();

            List<Order> newOrders = new List<Order>();

            Customer customer = StaticService.currentCustomer;

            foreach (Order order in orders)
            {
                if (order.CustomerId == customer.Id)
                {
                    newOrders.Add(order);
                }
            }

            if (newOrders.Count == 0)
            {
                Console.WriteLine("You have not ordered anything yet. Please go ahead and make some orders.");
                ContinueNext();
            }
            else
            {

                foreach (var currentOrder in newOrders)
                {
                    Console.WriteLine(currentOrder);
                    Console.WriteLine("==================================");


                }
                ContinueNext();
            }
        }

        private void GetDetails()
        {
            Console.WriteLine(StaticService.currentCustomer);
            ContinueNext();

        }

        private void ContinueNext()
        {
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }
    }
}