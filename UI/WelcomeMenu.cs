using System;
using System.Runtime.CompilerServices;
using Models;
using System.Collections.Generic;
using StoreBL;
using Microsoft.IdentityModel.Tokens;

namespace UI
{
    public class WelcomeMenu : IMenu
    {
        private IBL _bl;

        private StoreService _storeService;

        public WelcomeMenu(IBL bl, StoreService storeService)
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
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] Browse items.");
                Console.WriteLine("[2] View your profile.");
                Console.WriteLine("[x] Go Back");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        selectALocation();
                        break;
                    case "2":
                        MenuFactory.GetMenu("profile").Start();
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

        private void selectALocation()
        {
            // selectALocation:
                // Console.WriteLine("Please select a location to shop at");
                List<StoreFront> allStores = _bl.GetAllStores();
                if(allStores == null || allStores.Count == 0)
                {
                    Console.WriteLine("Stores under constructions.");
                    return;
                }

                StoreFront selectedStore = _storeService.SelectAStore("Pick a store to shop at", allStores);

                Console.WriteLine("You Selected " + selectedStore);

                StaticService.currentStore = selectedStore; 
                MenuFactory.GetMenu("store").Start();

        }

       private void goToProfile()
       {
           
       }

        
    }
}