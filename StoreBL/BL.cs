using System;
using System.Collections.Generic;
using Models;
using DL;
using System.Text.RegularExpressions;

namespace StoreBL
{
    public class BL : IBL

    {
        private readonly IRepo _repo;

        public BL(IRepo repo)
        {
            _repo = repo;
        }
        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public Customer AddCustomer(Customer customer)
        {
            return _repo.AddCustomer(customer);
        }

        public Customer UpdateCustomer(Customer customerToUpdate)
        {
            return _repo.UpdateCustomer(customerToUpdate);
        }

        public List<Customer> SearchCustomer(string quertStr)
        {
            return _repo.SearchCustomer(quertStr);
        }

        public List<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
        }

        public List<Order> GetAllOrders()
        {
            return _repo.GetAllOrders();
        }

        public List<StoreFront> GetAllStores()
        {
            return _repo.GetAllStores();
        }

        public List<LineItems> GetLineItems()
        {
            return _repo.GetLineItems();
        }

        public void DeleteCustomer(string email)
        {
            _repo.DeleteCustomer(email);
        }

        public Product UpdateProduct(Product productToUpdate)
        {
            return _repo.UpdateProduct(productToUpdate);
        }

        public StoreFront UpdateStore(StoreFront storeToUpdate)
        {
            return _repo.UpdateStore(storeToUpdate);
        }

        public StoreFront AddStore(StoreFront storeFront)
        {
            return _repo.AddStore(storeFront);
        }

        public decimal CalculateTotal(decimal price, int moreItems)
        {
            decimal total = 0;
            total += moreItems;
            return total;
        }

        public Order AddOrder(Order order)
        {
            return _repo.AddAnOrder(order);
        }

        public Product AddProduct(Product product)
        {
            return _repo.AddProduct(product);
        }

        public List<Inventory> GetAllInventories()
        {
            return _repo.GettAllInventories();
        }

        public Customer GetCustomer(string name)
        {
            return _repo.GetCustomer(name);
        }

        public List<Inventory> GetInventoriesByStoreId(int storeId)
        {
            return _repo.GetInventoriesByStoreId(storeId);
        }

        public LineItems AddLineItem(LineItems itemToAdd)
        {
            return _repo.AddLineItem(itemToAdd);
        }

        public Inventory UpdateInventory(Inventory inventoryToUpdate)
        {

            return _repo.UpdateInventory(inventoryToUpdate);
        }

        public Order UpdateOrder(Order orderToUpdate)
        {
            return _repo.UpdateOrder(orderToUpdate);
        }

        public int ReduceQuantity(int itemQuantity, int quantityNeeded)
        {
            return itemQuantity - quantityNeeded;
        }

        public StoreFront GetStoreById(int storeId)
        {
            return _repo.GetStoreById(storeId);
        }

        public void DeleteStore(int storeId)
        {
            _repo.DeleteStore(storeId);
        }



        // public List<LineItems> AddLineItems(List<LineItems> lineItems)
        // {
        //     return _repo.AddLineItems(lineItems);
        // }

        // public List<LineItems> GetLineItemsByOrderId(int orderId)
        // {
        //     return _repo.GetLineItemsById(orderId);
        // }
    }
}
