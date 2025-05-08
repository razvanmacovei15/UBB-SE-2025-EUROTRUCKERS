using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2025_EUROTRUCKERS.Data;
using UBB_SE_2025_EUROTRUCKERS.Models;
using Xunit;

namespace UBB_SE_2025_EUROTRUCKERS.Tests
{
    public class RepositoryIntegrationTests
    {
        private TransportDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<TransportDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            return new TransportDbContext(options);
        }

        [Fact]
        public async Task AddAsync_AddsEntity()
        {
            using var context = GetInMemoryDbContext();
            var repo = new Repository<Delivery>(context);

            var delivery = new Delivery { delivery_id = 1, status = "Pending" };
            await repo.AddAsync(delivery);

            var result = await repo.GetByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Pending", result.status);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntity()
        {
            using var context = GetInMemoryDbContext();
            var repo = new Repository<Delivery>(context);

            var delivery = new Delivery { delivery_id = 2, status = "Pending" };
            await repo.AddAsync(delivery);

            delivery.status = "Completed";
            await repo.UpdateAsync(delivery);

            var result = await repo.GetByIdAsync(2);
            Assert.Equal("Completed", result.status);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntity()
        {
            using var context = GetInMemoryDbContext();
            var repo = new Repository<Delivery>(context);

            var delivery = new Delivery { delivery_id = 3, status = "Pending" };
            await repo.AddAsync(delivery);

            await repo.DeleteAsync(3);
            var result = await repo.GetByIdAsync(3);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEntities()
        {
            using var context = GetInMemoryDbContext();
            var repo = new Repository<Delivery>(context);

            await repo.AddAsync(new Delivery { delivery_id = 4 });
            await repo.AddAsync(new Delivery { delivery_id = 5 });

            var result = await repo.GetAllAsync();
            Assert.Equal(2, result.Count());
        }
    }
}
