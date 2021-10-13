using System;
using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {
        /// <summary>
        ///  Adds Customer to the database.
        /// </summary>
        /// <param name="customer"> customer to be added</param>
        /// <returns>added customer</returns>
        Customer AddCustomer(Customer customer);
        /// <summary>
        /// Gets a customer by their name.
        /// </summary>
        /// <param name="name">customer</param>
        /// <returns>customer</returns>
        Customer GetCustomer(string name);
        /// <summary>
        /// Adds order to the database.
        /// </summary>
        /// <param name="order">order</param>
        /// <returns>added order</returns>
        Order AddAnOrder(Order order);
        /// <summary>
        /// Adds a product to the database.
        /// </summary>
        /// <param name="product">poduct</param>
        /// <returns>addedProduct</returns>
        Product AddProduct(Product product);
        /// <summary>
        /// Adds a store to the database.
        /// </summary>
        /// <param name="storeFront">Store</param>
        /// <returns>added store</returns>
        StoreFront AddStore(StoreFront storeFront);
        /// <summary>
        /// Adds lineitems to the database.
        /// </summary>
        /// <param name="lineItems">lineitems</param>
        /// <returns>added lineitem</returns>
        LineItems AddLineItem(LineItems lineItems);
        /// <summary>
        /// Adds inventory to database.
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        Inventory AddInventory(Inventory inventory);
        /// <summary>
        /// Gets all customer from the database.
        /// </summary>
        /// <returns>list of all customers.</returns>
        List<Customer> GetAllCustomers();
        /// <summary>
        /// Gets all products from the database.
        /// </summary>
        /// <returns>list of all products</returns>
        List<Product> GetAllProducts();
        /// <summary>
        /// Gets a product by their id from the database.
        /// </summary>
        /// <param name="productId">int</param>
        /// <returns>A product.</returns>
        Product GetProductById(int productId);
        /// <summary>
        /// Gets all orders from the database
        /// </summary>
        /// <returns>list of orders.</returns>
        List<Order> GetAllOrders();
        List<LineItems> GetLineItems();
        List<StoreFront> GetAllStores();
        List<Inventory> GettAllInventories();
        void DeleteCustomer(string name);

        void DeleteStore(int storeId);

        void DeleteProduct(int productId);
        void DeleteInventory(int inventoryId);
        Customer UpdateCustomer(Customer customerToUpdate);
        Product UpdateProduct(Product productToUpdate);

        Order UpdateOrder(Order orderToUpdate);

        StoreFront UpdateStore(StoreFront storeToUpdate);
        List<Customer> SearchCustomer(string queryStr);

        Inventory UpdateInventory(Inventory inventoryToUpdate);

        List<Inventory> GetInventoriesByStoreId(int storeId);

        StoreFront GetStoreById(int storeId);

        Inventory GetInventoryById(int inventoryId);

    }
}
