using System;
using System.Collections.Generic;
using StoreBL;
using Models;
using System.Globalization;
using System.Linq;

namespace UI
{
    public class AdminMenu : IMenu
    {
        private IBL _bl;

        private StoreService _storeService;

        public AdminMenu(IBL bl, StoreService storeService)
        {
            _bl = bl;
            _storeService = storeService;
        }
        public void Start()
        {
            Console.WriteLine("Welcome big boss!!!");
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("[0] Create Store");
                Console.WriteLine("[1] View all users.");
                Console.WriteLine("[2] View all Products.");
                Console.WriteLine("[3] View all Store.");
                Console.WriteLine("[4] Add New Products");
                Console.WriteLine("[5] Restock inventory.");
                Console.WriteLine("[6] View Inventory.");
                Console.WriteLine("[x] Back to Main menu.");
                input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        CreateStore();
                        break;
                    case "1":
                        GetAllCustomers();
                        break;
                    case "2":
                        GetAllProducts();
                        break;
                    case "3":
                        GetAllStores();
                        break;
                    case "4":
                        // SelectAProduct();
                        AddNewProducts();
                        break;
                    case "5":
                        RestockInventory();
                        break;
                    case "6":
                        ViewInventory();
                        break;
                    case "x":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }

            } while (!exit);
        }

        private void GetAllCustomers()
        {
            List<Customer> customers = _bl.GetAllCustomers();
            foreach (Customer cust in customers)
            {
                Console.WriteLine(cust);
                Console.WriteLine();
            }
        }

        private void GetAllProducts()
        {
            List<Product> products = _bl.GetAllProducts();
            foreach (Product product in products)
            {
                Console.WriteLine(product);
                Console.WriteLine();
            }
        }

        private void SelectAProduct()
        {
            Console.WriteLine("Select a product to get details.");
            List<Product> allProducts = _bl.GetAllProducts();
            Product selectedProduct = _storeService.SelectAProduct("Pick a product", allProducts);

            Console.WriteLine("You Selected " + selectedProduct);
        }

        private void CreateStore()
        {
            Console.WriteLine("Creating new Store");
            StoreFront newStore = new StoreFront();
        inputName:
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();

            try
            {
                newStore.Name = name;
            }
            catch (InputInvalidException e)
            {
                Console.WriteLine(e.Message);
                goto inputName;
            }
            Console.WriteLine("Address: ");
            newStore.Address = Console.ReadLine();

            StoreFront addedStore = _bl.AddStore(newStore);
            Console.WriteLine($"You created {addedStore}");
        }

        private void GetAllStores()
        {
            List<StoreFront> stores = _bl.GetAllStores();
            foreach (StoreFront store in stores)
            {
                Console.WriteLine(store);
                Console.WriteLine();
            }
        }

        private void AddNewProducts()
        {
            Console.WriteLine("Creating new product...");
            Product newProduct = new Product();
            Console.WriteLine("Add product name: ");
            newProduct.Name = Console.ReadLine();

            Console.WriteLine("Add product Description:");
            newProduct.Description = Console.ReadLine();
            Console.WriteLine("Add product Price:");
            newProduct.Price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Add product Category:");
            newProduct.Category = Console.ReadLine();

            _bl.AddProduct(newProduct);
            Console.WriteLine("Product added successfully");
        }

        // private void AddInventory()
        // {
        //     List<Product> listOfProducts = _bl.GetAllProducts();
        //     Product product = _storeService.SelectAProduct("Select the product to add inventory ", listOfProducts);


        // }

        private void RestockInventory()
        {

            List<StoreFront> allStores = _bl.GetAllStores();
            if(allStores == null && allStores.Equals(""))
            {
                Console.WriteLine("You don't have a store yet.");
            }

            StoreFront store = _storeService.SelectAStore("Select a store to update inventory", allStores);

            List<Inventory> allInventories = _bl.GetInventoriesByStoreId(store.Id);

            if(allInventories == null || allInventories.Equals(""))
            {
                Console.WriteLine("Empty inventory. Stop being lazy and do the inventories.");
            }

            Inventory selectedItem = _storeService.SelectAnItem("Select the inventory to restock", allInventories);
            Console.WriteLine(selectedItem);

            Console.WriteLine("How many quantity do you want to add?");
            int quantity = Int32.Parse(Console.ReadLine());

            selectedItem.Quantity += quantity;

            _bl.UpdateInventory(selectedItem);

            Console.WriteLine("Inventory successfully updated.");
            
        }

        private void ViewInventory()
        {
            List<StoreFront> allStores = _bl.GetAllStores();
            if (allStores == null && allStores.Equals(""))
            {
                Console.WriteLine("You don't have a store yet.");
            }

            StoreFront store = _storeService.SelectAStore("Which store inventory are you checking?", allStores);
            int storeId = store.Id;
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
    }
}