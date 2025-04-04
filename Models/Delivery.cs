using System;

namespace UBB_SE_2025_EUROTRUCKERS.Models
{
    public class Delivery
    {
        public int delivery_id { get; set; }
        public string reference_number { get; set; }
        public string departure_address { get; set; }
        public string destination_address { get; set; }
        public DateTime departure_time { get; set; }
        public DateTime estimated_time_arrival { get; set; }
        public string status { get; set; }
        public int driver_id { get; set; }
        public Driver driver { get; set; }
        public int truck_id { get; set; }
        public Truck truck { get; set; }
        public int company_id { get; set; }
        public Company company { get; set; }
        public string cargo_description { get; set; }
        public decimal weight_kg { get; set; }
        public decimal total_distance_km { get; set; }
        public decimal fee_euros { get; set; }
    }
}
