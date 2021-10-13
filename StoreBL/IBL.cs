using System;
using System.Collections.Generic;
using System.Net.Security;
using Models;

namespace StoreBL
{
    public interface IBL
    {
        /// <summary>
        /// Gets all customer from the database.
        /// </summary>
        /// <returns>list of all customers.</returns>
        List<Customer> GetAllCustomers();
        /// <summary>
        ///  Adds Customer to the database.
        /// </summary>
        /// <param name="customer"> customer to be added</param>
        /// <returns>added customer</returns>
        Customer AddCustomer(Customer customer);
        /// <summary>
        /// Adds a store to the database.
        /// </summary>
        /// <param name="storeFront">Store</param>
        /// <returns>added store</returns>
        StoreFront AddStore(StoreFront storeFront);
        /// <summary>
        /// Adds order to the database.
        /// </summary>
        /// <param name="order">order</param>
        /// <returns>added order</returns>
        Order AddOrder(Order order);
        /// <summary>
        /// Adds a product to the database.
        /// </summary>
        /// <param name="product">poduct</param>
        /// <returns>addedProduct</returns>
        Product AddProduct(Product product);
        /// <summary>
        /// Adds lineitems to the database.
        /// </summary>
        /// <param name="lineItems">lineitems</param>
        /// <returns>added lineitem</returns>
        LineItems AddLineItem(LineItems itemToAdd);
        /// <summary>
        /// Adds inventory to database.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        Inventory AddInventory(Inventory inventoryToAdd);

        // List<LineItems> AddLineItems(List<LineItems> lineItems);
        /// <summary>
        /// Gets a customer by their name.
        /// </summary>
        /// <param name="name">customer</param>
        /// <returns>customer</returns>
        Customer GetCustomer(string name);
        /// <summary>
        /// Gets a product by their id from the database.
        /// </summary>
        /// <param name="productId">int</param>
        /// <returns>A product.</returns>
        Product GetProductById(int productId);
        List<Inventory> GetInventoriesByStoreId(int storeId);
        Customer UpdateCustomer(Customer customerToUpdate);
        Product UpdateProduct(Product productToUpdate);

        Inventory UpdateInventory(Inventory inventoryToUpdate);

        Order UpdateOrder(Order orderToUpdate);
        StoreFront UpdateStore(StoreFront storeToUpdate);
        void DeleteCustomer(string email);

        void DeleteStore(int storeId);

        void DeleteProduct(int productId);
        void DeleteInventory(int inventoryId);

        List<Customer> SearchCustomer(string quertStr);
        /// <summary>
        /// Gets all products from the database.
        /// </summary>
        /// <returns>list of all products</returns>
        List<Product> GetAllProducts();
        /// <summary>
        /// Gets all orders from the database
        /// </summary>
        /// <returns>list of orders.</returns>
        List<Order> GetAllOrders();
        List<StoreFront> GetAllStores();
        List<Inventory> GetAllInventories();
        List<LineItems> GetLineItems();
        decimal CalculateTotal(decimal price, int moreItems);

        int ReduceQuantity(int itemQuantity, int quantityNeeded);

        StoreFront GetStoreById(int storeId);
        Inventory GetInventoryById(int inventoryId);


    }
}