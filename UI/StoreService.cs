using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Models;

namespace UI
{
    public class StoreService
    {
        public Product SelectAProduct(string prompt, List<Product> listToPick)
        {
        SelectOrder:
            Console.WriteLine(prompt);
            for (int i = 0; i < listToPick.Count; i++)
            {
                Console.WriteLine($"[{i}] {listToPick[i]}");
            }

            string input = Console.ReadLine();
            int parsedInput;

            bool parseSuccess = Int32.TryParse(input, out parsedInput);

            if (parseSuccess && parsedInput >= 0 && parsedInput < listToPick.Count)
            {
                return listToPick[parsedInput];
            }
            else
            {
                Console.WriteLine("Invalid input.");
                goto SelectOrder;
            }

        }

        public StoreFront SelectAStore(string prompt, List<StoreFront> listToPick)
        {
        SelectAStore:
            for (int i = 0; i < listToPick.Count; i++)
            {
                Console.WriteLine($"[{i}] {listToPick[i]}");
            }
            Console.WriteLine(prompt);

            string input = Console.ReadLine();
            int parsedInput;

            bool parseSuccess = Int32.TryParse(input, out parsedInput);

            if (parseSuccess && parsedInput >= 0 && parsedInput < listToPick.Count)
            {
                return listToPick[parsedInput];
            }
            else
            {
                Console.WriteLine("Invalid input.");
                goto SelectAStore;
            }

        }

        public Inventory SelectAnItem(string prompt, List<Inventory> inventories)
        {
        selectAnItem:
            for (int i = 0; i < inventories.Count; i++)
            {
                Console.WriteLine($"[{i}] {inventories[i]} ");
            }
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            int parsedInput;
            bool parseSuccess = Int32.TryParse(input, out parsedInput);

            if (parseSuccess && parsedInput >= 0 && parsedInput < inventories.Count)
            {
                return inventories[parsedInput];
            }
            else
            {
                Console.WriteLine("Invalid input.");
                goto selectAnItem;
            }

        }
    }
}