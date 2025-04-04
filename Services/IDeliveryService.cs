using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UBB_SE_2025_EUROTRUCKERS.Data;
using UBB_SE_2025_EUROTRUCKERS.Models;

namespace UBB_SE_2025_EUROTRUCKERS.Services
{
    public interface IDeliveryService
    {
        Task<IEnumerable<Delivery>> GetAllDeliveriesAsync();
        Task<Delivery> GetDeliveryDetailsAsync(int id);
        Task CreateDeliveryAsync(Delivery delivery);
        Task UpdateDeliveryStatusAsync(int deliveryId, string status);
    }

    // Services/DeliveryService.cs
    public class DeliveryService : IDeliveryService
    {
        private readonly IRepository<Delivery> _deliveryRepository;

        public DeliveryService(IRepository<Delivery> deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<IEnumerable<Delivery>> GetAllDeliveriesAsync()
        {
            var deliveries = await _deliveryRepository.GetAllAsync();

            if (deliveries == null || !deliveries.Any())
            {
                Console.WriteLine("No deliveries found in repository.");
            }

            return deliveries;
        }

        public async Task<Delivery> GetDeliveryDetailsAsync(int id)
        {
            return await _deliveryRepository.GetByIdAsync(id);
        }

        public async Task CreateDeliveryAsync(Delivery delivery)
        {
            await _deliveryRepository.AddAsync(delivery);
        }

        public async Task UpdateDeliveryStatusAsync(int deliveryId, string status)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
            if (delivery != null)
            {
                delivery.status = status;
                await _deliveryRepository.UpdateAsync(delivery);
            }
        }
    }
}
