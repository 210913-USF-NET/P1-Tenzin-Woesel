using System;
using System.Collections.Generic;
using System.Net.Security;
using Models;

namespace StoreBL
{
    public interface IBL
    {
        List<Customer> GetAllCustomers();
        Customer AddCustomer(Customer customer);
        StoreFront AddStore(StoreFront storeFront);
        Order AddOrder(Order order);
        Product AddProduct(Product product);

        LineItems AddLineItem(LineItems itemToAdd);
        Inventory AddInventory(Inventory inventoryToAdd);

        // List<LineItems> AddLineItems(List<LineItems> lineItems);
        Customer GetCustomer(string name);

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
        List<Product> GetAllProducts();

        List<Order> GetAllOrders();
        List<StoreFront> GetAllStores();
        List<Inventory> GetAllInventories();
        List<LineItems> GetLineItems();

        // List<LineItems> GetLineItemsByOrderId(int OrderId);

        decimal CalculateTotal(decimal price, int moreItems);

        int ReduceQuantity(int itemQuantity, int quantityNeeded);

        StoreFront GetStoreById(int storeId);
        Inventory GetInventoryById(int inventoryId);


    }
}