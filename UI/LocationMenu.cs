using System;
using StoreBL;

namespace UI
{
    public class LocationMenu : IMenu
    {
        private IBL _bl;

        public LocationMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {

            Console.WriteLine("Please select a Location to shop from");

            bool exit = false;
            string userInput = "";
            do
            {
                Console.Write("Show lists of locations");
                userInput = Console.ReadLine();
                switch(userInput)
                {
                    case "1":
                        ViewProducts();
                        break;
                    case "3":
                        Console.WriteLine("Go back to previous menu");
                        break;
                    case "x":
                        MenuFactory.GetMenu("main menu");
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            } while (!exit);
        }

        private void ViewProducts()
        {
            
        }
    }
}