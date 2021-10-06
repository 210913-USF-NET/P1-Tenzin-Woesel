using System;
using DL;
using StoreBL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq.Expressions;

namespace UI
{
    public class MenuFactory
    {


        public static IMenu GetMenu(string menuString)
        {
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<P0TenzinStoreContext> option = new DbContextOptionsBuilder<P0TenzinStoreContext>().UseSqlServer(connectionString).Options;
            P0TenzinStoreContext context = new P0TenzinStoreContext(option);
            switch (menuString.ToLower())
            {
                case "main":
                    return new MainMenu();
                case "current user":
                    return new CurrentUserMenu(new BL(new DBCustomerRepo(context)));
                case "new user":
                    return new NewUserMenu(new BL(new DBCustomerRepo(context)));
                case "welcome menu":
                    return new WelcomeMenu(new BL(new DBCustomerRepo(context)), new StoreService());
                case "admin":
                    return new AdminMenu(new BL(new DBCustomerRepo(context)), new StoreService());
                case "location menu":
                    return new LocationMenu(new BL(new DBCustomerRepo(context)));
                case "store":
                    return new StoreMenu(new BL(new DBCustomerRepo(context)), new StoreService());
                // case "items":
                //     return new LineItemMenu(new BL(new DBCustomerRepo(context)));
                case "profile":
                    return new ProfileMenu(new BL(new DBCustomerRepo(context)), new StoreService());
                default:
                    return null;
            }
        }
    }
}