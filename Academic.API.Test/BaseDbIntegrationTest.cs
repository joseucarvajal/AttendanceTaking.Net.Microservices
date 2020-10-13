﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.IO;
using Xunit;

namespace Academic.API.Test
{
    public class BaseDbIntegrationTest<TStartup>
        : IClassFixture<WebApplicationFactory<TStartup>>
        where TStartup : class
        
    {
        protected readonly WebApplicationFactory<TStartup> Factory;

        public BaseDbIntegrationTest(WebApplicationFactory<TStartup> factory)
        {
            var configPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            Factory = factory.WithWebHostBuilder(builder => {
                builder.ConfigureAppConfiguration((context, config) => {
                    config.AddJsonFile(configPath);
                });
            });
        }
    }
}
