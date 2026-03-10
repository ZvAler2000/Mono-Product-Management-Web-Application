using System;
using System.Collections.Generic;
using System.Text;
using Project.Service.Data;
using Microsoft.EntityFrameworkCore;

namespace ProductWebAPITester
{
    public static class ProductDBContextFactory
    {
        public static ProductDBContext Create()
        {
            var options = new DbContextOptionsBuilder<ProductDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ProductDBContext(options);
        }
    }
}
