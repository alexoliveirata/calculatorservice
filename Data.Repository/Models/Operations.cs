using System;

namespace Data.Repository.Models
{
    public class Operations
    {
        public int Id { get; set; }

        public string TrackingId { get; set; }
        public string Operation { get; set; }
        public string Calculation { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}