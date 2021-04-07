using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Domain
{
    public enum ReservationStatus { Pending, Ready, Denied }
    public class BookReservation
    {
        public int Id { get; set; }
        public string For { get; set; }
        public string BookIds { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
