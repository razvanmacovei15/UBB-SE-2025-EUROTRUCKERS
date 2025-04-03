using System;

namespace UBB_SE_2025_EUROTRUCKERS.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public string ReferenceNumber { get; set; }
        public string DepartureAddress { get; set; }
        public string DestinationAddress { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime EstimatedTimeArrival { get; set; }
        public string Status { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int TruckId { get; set; }
        public Truck Truck { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string CargoDescription { get; set; }
        public decimal WeightKg { get; set; }
        public decimal TotalDistanceKm { get; set; }
        public decimal FeeEuros { get; set; }
        public string Notes { get; set; }
    }
}
