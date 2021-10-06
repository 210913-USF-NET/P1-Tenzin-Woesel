using System;
using System.Runtime.CompilerServices;
using Models;
using System.Collections.Generic;
using StoreBL;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.Data.SqlClient.Server;
using System.Net.NetworkInformation;
using Serilog;
using System.ComponentModel;

namespace UI
{
    public class StoreMenu : IMenu
    {
        private IBL _bl;

        private StoreService _storeService;
        private List<LineItems> items = new List<LineItems>();

        public StoreMenu(IBL bl, StoreService storeService)
        {
            _bl = bl;
            _storeService = storeService;
        }
        public void Start()
        {
            bool exit = false;
            string userInput = "";
            do
            {
                Console.WriteLine("****** Snow Lion Stores ******");
                //Show the lists of items and then show the options of adding to cart
                Console.WriteLine("[1] View Items");
                Console.WriteLine("[2] Select Another Location");
                Console.WriteLine("[3] Add item.");
                Console.WriteLine("[4] Go back to previous menu.");
                Console.WriteLine("[x] Back to Main Menu.");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        ViewItems();
                        break;
                    case "2":
                        SelectAnotherLocation();
                        MenuFactory.GetMenu("store").Start();
                        break;
                    case "3":
                        AddItemToOrder();
                        break;
                    case "4":
                        exit = true;
                        break;
                    case "admin":
                        MenuFactory.GetMenu("admin").Start();
                        break;
                    case "x":
                        MenuFactory.GetMenu("main").Start();
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }


            } while (!exit);
        }

        private void ViewItems()
        {
            int storeId = StaticService.currentStore.Id;
            List<Inventory> inventories = _bl.GetInventoriesByStoreId(storeId);

            // foreach (var i in inventories)
            // {
            //     System.Console.WriteLine(i);
            // }

            List<Product> allProducts = _bl.GetAllProducts();


            //create a collection of inv prods pair. Each element in collection is
            //anonymous type containing both the products name and inventory id
            var tempInventory = from inv in inventories
                                join prods in allProducts on inv.ProductID equals prods.Id
                                select new { inv.Id, inv.ProductID, prods.Name, inv.Quantity, prods.Price, prods.Description };

            foreach (var newIn in tempInventory)
            {

                Console.WriteLine($"Inventory Id : {newIn.Id}");
                Console.WriteLine($"Product Id : {newIn.ProductID}");
                Console.WriteLine("Item Name : " + newIn.Name);
                Console.WriteLine("Description : " + newIn.Description);
                Console.WriteLine($"Items left : {newIn.Quantity}\n");
            }

        }

        private void StartAnOrder()
        {
            int storeId = StaticService.currentStore.Id;
            int customerId = StaticService.currentCustomer.Id;

            Order newOrder = new Order();
            newOrder.CustomerId = customerId;
            newOrder.StoreFrontId = storeId;

            newOrder = _bl.AddOrder(newOrder);

            StaticService.currentOrder = newOrder;
        }

        private void SelectAnotherLocation()
        {
            List<StoreFront> allStores = _bl.GetAllStores();
            if (allStores == null || allStores.Count == 0)
            {
                Console.WriteLine("Stores under constructions.");
                return;
            }

            StoreFront selectedStore = _storeService.SelectAStore("Pick a store to shop at", allStores);

            Console.WriteLine("You Selected " + selectedStore);

            StaticService.currentStore = selectedStore;
        }

        private void AddItemToOrder()
        {
            StartAnOrder();
            Order currentOrder = StaticService.currentOrder;

            int storeId = StaticService.currentStore.Id;
            List<Inventory> inventories = _bl.GetInventoriesByStoreId(storeId);
            Console.WriteLine(" ===========================");
        selectAnItem:
            Inventory selectedItem = _storeService.SelectAnItem("Pick an item to add to order : ", inventories);

            Console.WriteLine("You selected " + selectedItem);

            LineItems item = new LineItems();

            item.ProductId = selectedItem.ProductID;

            item.OrderId = currentOrder.Id;

            Console.WriteLine("How many do you want to add?");
            int quantityNeeded = int.Parse(Console.ReadLine());
            // Console.WriteLine(item.Quantity);
            selectedItem.Quantity = _bl.reduceQuantity(selectedItem.Quantity, quantityNeeded);

            // selectedItem.Quantity -= quantityNeeded;

            Console.WriteLine("Do you want to add more items? Y/N");
            string moreItems = Console.ReadLine();
            if (moreItems.ToLower() == "y")
            {
            
                decimal total = _bl.CalculateTotal(selectedItem.Product.Price, quantityNeeded);
                currentOrder.Total = total;
                Console.WriteLine("Total is : " + total);
                goto selectAnItem;
            }
            else
            {
                item.Quantity = quantityNeeded;
                item = _bl.AddLineItem(item);
                items.Add(item);
                currentOrder.LineItems = items;
                decimal total = _bl.CalculateTotal(selectedItem.Product.Price, quantityNeeded);
                // decimal newHowMany = Decimal.Parse(moreItems);
                // total = total * newHowMany;

                List<Product> allProducts = _bl.GetAllProducts();
                currentOrder.Total = total;
                var tempItems = from i in items
                                join p in allProducts on i.ProductId equals p.Id
                                select new { p.Name, p.Description, i.Quantity };
                Console.WriteLine("Your order contains: ");
                foreach (var newInv in tempItems)
                {
                    Console.WriteLine("Item Name: " + newInv.Name);
                    Console.WriteLine("Quantity: " + newInv.Quantity);
                }
                Console.WriteLine("Total is : " + total);
                Console.WriteLine("================");
                //update the order, product and item to repo.
                _bl.UpdateInventory(selectedItem);
                _bl.UpdateOrder(currentOrder);
                Log.Information("Order placed successfully");
                Console.WriteLine("Thank you for shopping with us.");
                currentOrder = new Order();
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                // int id = currentOrder.Id;
                // List<LineItems> toDisplayToUser = _bl.GetLineItems();
            }

        }
    }
}