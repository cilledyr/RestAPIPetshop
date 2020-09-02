using Petshop.Core.Enteties;
using Petshop.Core;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Petshop.Core.ApplicationService;
using Petshop.Core.ApplicationService.Impl;
using Petshop.Core.DomainService;
using Petshop.Infrastructure.Data;
using System.Security.Authentication.ExtendedProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Petshop.UI
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            bool devMode = true;
            var services = new ServiceCollection();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerService, OwnerService>();

            var provider = services.BuildServiceProvider();

            var ownerRepo = provider.GetService<IOwnerRepository>();
            var petRepo = provider.GetService<IPetRepository>();
            if(devMode)
            {
                var dataInit = new DataInitializer(ownerRepo, petRepo);
                Console.WriteLine(dataInit.InitData()); //I prefer the program telling me that i have injected data.
            }
            
            var petService = provider.GetService<IPetService>();
            var ownerService = provider.GetService<IOwnerService>();

            var printer = new Printer(petService, ownerService);

            Console.WriteLine("Welcome to the Petshop please type your name:");
            
            var userName = Console.ReadLine();
            printer.DisplayMenu(userName);
        }

        

        
    }
}
