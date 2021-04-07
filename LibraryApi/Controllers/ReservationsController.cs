using LibraryApi.Filters;
using LibraryApi.Models.Reservations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class ReservationsController : ControllerBase
    {
        private readonly ILookupReservations _reservationLookups;
        private readonly IReservationCommands _reservationCommands;

        public ReservationsController(ILookupReservations reservationLookups, IReservationCommands reservationCommands)
        {
            _reservationLookups = reservationLookups;
            _reservationCommands = reservationCommands;
        }

        // POST a reservation
        [HttpPost("/reservations")]
        [ValidateModel]
        public async Task<ActionResult> AddAReservation([FromBody] PostReservationRequest request)
        {
            // Decide whether it is worthy of a post
            GetReservationSummaryResponseItem response = await _reservationCommands.AddReservationAsync(request);
            return CreatedAtRoute("reservations#getbyid", new { Id = response.Id }, response);
        }
        // GET /{id}
        [HttpGet("/reservations/{id:int}", Name ="reservations#getbyid")]
        public async Task<ActionResult> GetReservationById(int id)
        {
            GetReservationSummaryResponseItem response = await _reservationLookups.GetByIdAsync(id);
            return this.Maybe(response);
        }
        // GET /all
        [HttpGet("/reservations")]
        public async Task<ActionResult> GetAllReservations()
        {
            GetReservationSummaryResponse response = await _reservationLookups.GetAllReservationsAsync();
            return Ok(response); 
        }
        // GET /pending
        // GET /ready
        // POST /ready (worker)
        // GET /denied
        // POST /denied (worker)
    }
}
