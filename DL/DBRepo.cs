using System;
using System.Collections.Generic;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.Common;
using System.Threading.Tasks;

namespace DL
{
    public class DBRepo : IRepo
    {
        private readonly StoreDBContext _context;

        public DBRepo(StoreDBContext context)
        {
            _context = context;
        }
        public Order AddAnOrder(Order order)
        {
            order = _context.Orders.Add(order).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return order;
        }

        public Customer AddCustomer(Customer customer)
        {
            customer = _context.Add(customer).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return customer;
        }
        public Product AddProduct(Product product)

        {
            product = _context.Add(product).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return product;
        }

        public void DeleteCustomer(string name)
        {
            Customer customerToDelete = GetCustomer(name);
            _context.Remove(customerToDelete);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.Select(customer => new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Email = customer.Email
            }).ToList();
        }

        public Customer GetCustomer(string name)
        {
            Customer customerByName = _context.Customers.FirstOrDefault(s => s.Name == name);

            if (customerByName == null)
            {
                return null;
            }
            else
            {
                return customerByName;
            }
        }

        public Customer UpdateCustomer(Customer customerToUpdate)
        {
            customerToUpdate = _context.Customers.Update(customerToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return customerToUpdate;
        }

        public List<Customer> SearchCustomer(string queryStr)
        {
            return _context.Customers.Where(
                custo => custo.Name.Contains(queryStr)).Select(
                    c => new Customer()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Address = c.Address,
                        Email = c.Email
                    }
                ).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Select(product => new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category

            }).ToList();
        }
        public List<StoreFront> GetAllStores()
        {
            return _context.StoreFronts.Select(stores => new StoreFront()
            {
                Id = stores.Id,
                Name = stores.Name,
                Address = stores.Address,
                Inventories = stores.Inventories,
                Orders = stores.Orders
            }).ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.Select(order => new Order()
            {
                Id = order.Id,
                Total = order.Total,
                OrderDate = order.OrderDate.ToLocalTime(),
                CustomerId = order.CustomerId,
                StoreFrontId = order.StoreFrontId
            }).ToList();
        }

        public List<LineItems> GetLineItems()
        {
            return _context.LineItems.Select(items => new LineItems()
            {
                Id = items.Id,
                Quantity = items.Quantity
            }).ToList();
        }


        public List<Inventory> GettAllInventories()
        {
            return _context.Inventories.Include("Product").Select(inventory => new Inventory()
            {
                Quantity = inventory.Quantity,
                ProductID = inventory.ProductID,
                StoreID = inventory.StoreID,
                Product = new Product
                {
                    Id = inventory.Product.Id,
                    Name = inventory.Product.Name,
                    Description = inventory.Product.Description,
                    Price = inventory.Product.Price,
                    Category = inventory.Product.Category
                }
            }).ToList();
        }


        public Product UpdateProduct(Product productToUpdate)
        {
            productToUpdate = _context.Products.Update(productToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return productToUpdate;
        }

        public StoreFront UpdateStore(StoreFront storeToUpdate)
        {
            storeToUpdate = _context.Update(storeToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return storeToUpdate;
        }


        public StoreFront AddStore(StoreFront storeFront)
        {

            storeFront = _context.Add(storeFront).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return storeFront;
        }

        public LineItems AddLineItem(LineItems lineItems)
        {
            lineItems = _context.Add(lineItems).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return lineItems;
        }

        public Inventory AddInventory(Inventory inventory)
        {
            inventory = _context.Add(inventory).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return inventory;
        }

        public List<Inventory> GetInventoriesByStoreId(int storeId)
        {
            return _context.Inventories.Where(inv => inv.StoreID == storeId).Select(newInv => new Inventory()
            {
                Id = newInv.Id,
                StoreID = newInv.StoreID,
                Quantity = newInv.Quantity,
                ProductID = newInv.ProductID
            }).ToList();
        }

        public Product GetProductById(int productId)
        {
            Product productById = _context.Products.FirstOrDefault(p => p.Id == productId);

            return productById;
        }

        public Inventory UpdateInventory(Inventory inventoryToUpdate)
        {
            inventoryToUpdate = _context.Inventories.Update(inventoryToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return inventoryToUpdate;
        }

        public Order UpdateOrder(Order orderToUpdate)
        {
            orderToUpdate = _context.Orders.Update(orderToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return orderToUpdate;
        }

        public StoreFront GetStoreById(int storeId)
        {
            StoreFront storeById = _context.StoreFronts.Include(s => s.Inventories).FirstOrDefault(s => s.Id == storeId);
            return storeById;
        }

        public Inventory GetInventoryById(int inventoryId)
        {
            Inventory inventoryById = _context.Inventories.FirstOrDefault(i => i.Id == inventoryId);
            return inventoryById;
        }
        /// <summary>
        /// Deletes a Store
        /// </summary>
        /// <param name="storeId"> id of the store to be deleted. </param>
        public void DeleteStore(int storeId)
        {
            _context.StoreFronts.Remove(GetStoreById(storeId));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public void DeleteProduct(int productId)
        {
            _context.Products.Remove(GetProductById(productId));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public void DeleteInventory(int inventoryId)
        {
            _context.Inventories.Remove(GetInventoryById(inventoryId));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
    }
}