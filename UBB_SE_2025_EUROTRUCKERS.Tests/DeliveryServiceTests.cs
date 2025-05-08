using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using UBB_SE_2025_EUROTRUCKERS.Services;
using UBB_SE_2025_EUROTRUCKERS.Models;
using UBB_SE_2025_EUROTRUCKERS.Data;

namespace UBB_SE_2025_EUROTRUCKERS.Tests
{
 

    public class DeliveryServiceTests
    {
        [Fact]
        public async Task GetAllDeliveriesAsync_ReturnsAllDeliveries()
        {
            var mockRepo = new Mock<IRepository<Delivery>>();
            var mockLogger = new Mock<ILoggingService>();

            var deliveries = new List<Delivery> {
            new Delivery { delivery_id = 1 },
            new Delivery { delivery_id = 2 }
        };

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(deliveries);

            var service = new DeliveryService(mockRepo.Object, mockLogger.Object);

            var result = await service.GetAllDeliveriesAsync();

            Assert.Equal(2, result.Count());
            mockLogger.Verify(l => l.LogDebug(It.Is<string>(s => s.Contains("Retrieved"))), Times.Once);
        }

        [Fact]
        public async Task GetDeliveryByIdAsync_DeliveryExists_ReturnsDelivery()
        {
            var mockRepo = new Mock<IRepository<Delivery>>();
            var mockLogger = new Mock<ILoggingService>();

            var delivery = new Delivery { delivery_id = 1 };
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(delivery);

            var service = new DeliveryService(mockRepo.Object, mockLogger.Object);

            var result = await service.GetDeliveryByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.delivery_id);
            mockLogger.Verify(l => l.LogWarning(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task GetDeliveryByIdAsync_DeliveryNotFound_LogsWarning()
        {
            var mockRepo = new Mock<IRepository<Delivery>>();
            var mockLogger = new Mock<ILoggingService>();

            mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Delivery)null);

            var service = new DeliveryService(mockRepo.Object, mockLogger.Object);

            var result = await service.GetDeliveryByIdAsync(99);

            Assert.Null(result);
            mockLogger.Verify(l => l.LogWarning("Delivery with ID 99 not found"), Times.Once);
        }

        [Fact]
        public async Task UpdateDeliveryStatusAsync_DeliveryExists_UpdatesStatus()
        {
            var mockRepo = new Mock<IRepository<Delivery>>();
            var mockLogger = new Mock<ILoggingService>();

            var delivery = new Delivery { delivery_id = 1, status = "Pending" };
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(delivery);

            var service = new DeliveryService(mockRepo.Object, mockLogger.Object);

            var result = await service.UpdateDeliveryStatusAsync(1, "Completed");

            Assert.True(result);
            mockRepo.Verify(r => r.UpdateAsync(It.Is<Delivery>(d => d.status == "Completed")), Times.Once);
            mockLogger.Verify(l => l.LogInformation(It.Is<string>(s => s.Contains("Updated delivery"))), Times.Once);
        }

        [Fact]
        public async Task CreateDeliveryAsync_AddsDelivery()
        {
            var mockRepo = new Mock<IRepository<Delivery>>();
            var mockLogger = new Mock<ILoggingService>();

            var delivery = new Delivery { delivery_id = 3 };
            var service = new DeliveryService(mockRepo.Object, mockLogger.Object);

            var result = await service.CreateDeliveryAsync(delivery);

            Assert.True(result);
            mockRepo.Verify(r => r.AddAsync(delivery), Times.Once);
            mockLogger.Verify(l => l.LogInformation(It.Is<string>(s => s.Contains("Created new delivery"))), Times.Once);
        }

        [Fact]
        public async Task DeleteDeliveryAsync_DeliveryExists_DeletesDelivery()
        {
            var mockRepo = new Mock<IRepository<Delivery>>();
            var mockLogger = new Mock<ILoggingService>();

            var delivery = new Delivery { delivery_id = 4 };
            mockRepo.Setup(r => r.GetByIdAsync(4)).ReturnsAsync(delivery);

            var service = new DeliveryService(mockRepo.Object, mockLogger.Object);

            var result = await service.DeleteDeliveryAsync(4);

            Assert.True(result);
            mockRepo.Verify(r => r.DeleteAsync(4), Times.Once);
            mockLogger.Verify(l => l.LogInformation(It.Is<string>(s => s.Contains("Deleted delivery"))), Times.Once);
        }
    }

}
