using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using VendingMachine;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTest
{
    public class PaymentTypeTestingWebAppFactory<T> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                if (dbContext != null)
                    services.Remove(dbContext);

                var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("vendingMachine");
                    options.UseInternalServiceProvider(serviceProvider);
                });
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    using (var appContext = scope.ServiceProvider.GetRequiredService<DataContext>())
                    {
                        try
                        {
                            appContext.Database.EnsureCreated();
                        }
                        catch (Exception ex)
                        {
                            //Log errors
                            throw;
                        }
                    }
                }
            });
        }
    }
}

