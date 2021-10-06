using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DL;
using Models;
using StoreBL;
using Serilog;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().WriteTo.File("../Logs/logs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

            Log.Information("Application Starting...");
            Console.WriteLine("Welcome to Snow Lion Store");
            new MainMenu().Start();

            // RAMCustomerRepo repo = RAMCustomerRepo.GetInstance();
            // new MainMenu(new BL(repo)).Start();

            Log.Information("Application Closed.");

            Log.CloseAndFlush();

        }
    }
}
