// using System;
// using System.Runtime.CompilerServices;
// using Models;
// using System.Collections.Generic;
// using StoreBL;

// namespace UI
// {
//     public class LineItemMenu : IMenu
//     {
//         private IBL _bl;

//         public LineItemMenu(IBL bl)
//         {
//             _bl = bl;
//         }
//         public void Start()
//         {
//             bool goBack = false;
//             string userInput = "";
//             do
//             {

//                 Console.WriteLine("Welcome to this Location");
//                 Console.WriteLine("These are the items available at this store");
//                 foreach (var product in ListOfProduct())
//                 {
//                     Console.WriteLine(product);
//                 }
//                 Console.WriteLine("[1] Add Item to cart");
//                 Console.WriteLine("[2] Remove Item from cart.");
//                 Console.WriteLine("[3] View Cart.");
//                 Console.WriteLine("[4] Check out.");
//                 Console.WriteLine("[5] Go Back to Store Menu");
//                 Console.WriteLine("[x] Go Back to Main Menu");
//                 userInput = Console.ReadLine();
//                 switch (userInput)
//                 {
//                     case "1":
//                         AddProduct();
//                         break;
//                     case "2":
//                         // RemoveItem();
//                         break;
//                     case "3":
//                         // ViewCart();
//                         break;
//                     case "4":
//                         // CheckOut();
//                         break;
//                     case "5":
//                         Console.WriteLine("Go back to previous menu.");
//                         goBack = true;
//                         break;
//                     default:
//                         Console.WriteLine("Invalid Input.");
//                         break;
//                 }

//             } while (!goBack);

//         }
//         private void AddProduct()
//         {
//         SelectAProduct:
//             Console.WriteLine("Select a product to add");
//             List<Product> allProducts = _bl.GetAllProducts();
//             if (allProducts == null || allProducts.Count == 0)
//             {
//                 Console.WriteLine("No products at this time :/");
//                 return;
//             }
//             for (int i = 0; i < allProducts.Count; i++)
//             {
//                 Console.WriteLine($"[{i}] {allProducts[i]}");
//             }

//             string input = Console.ReadLine();
//             int parsedInput;

//             bool parseSuccess = Int32.TryParse(input, out parsedInput);

//             if (parseSuccess && parsedInput >= 0 && parsedInput < allProducts.Count)
//             {
//                 Product selectedProduct = allProducts[parsedInput];
//                 Console.WriteLine($"You picked {selectedProduct.Name}");

//                 LineItems itemToAdd = new LineItems();

//             quantity:
//                 Console.WriteLine("How many do you want? ");
//                 int userChoice;
//                 bool success = int.TryParse(Console.ReadLine(), out userChoice);
//                 if (!success)
//                 {
//                     Console.WriteLine("Invalid input.");
//                     goto quantity;
//                 }

//                 try
//                 {
//                     itemToAdd.Quantity = userChoice;
//                 }
//                 catch (InputInvalidException e)
//                 {
//                     Console.WriteLine(e.Message);
//                     goto quantity;
//                 }
//                 finally
//                 {

//                 }

                

//                 Product updatedProduct = _bl.UpdateProduct(selectedProduct);

//             }
//             else
//             {
//                 Console.WriteLine("Invalid input.");
//                 goto SelectAProduct;
//             }


//         }

//         private List<Product> ListOfProduct()
//         {
//             return _bl.GetAllProducts();
//         }

//     }
// }