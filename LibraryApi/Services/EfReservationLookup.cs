using LibraryApi.Domain;
using LibraryApi.Models.Reservations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace LibraryApi.Services
{
    public class EfReservationLookup : ILookupReservations, IReservationCommands
    {
        private readonly LibraryDataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;
        private readonly IProcessReservations _reservationProcessor;
        public EfReservationLookup(LibraryDataContext context, IMapper mapper, MapperConfiguration config, IProcessReservations reservationProcessor)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _reservationProcessor = reservationProcessor;
        }

        public async Task<GetReservationSummaryResponseItem> AddReservationAsync(PostReservationRequest request)
        {

            var reservation = new BookReservation
            {
                For = request.For,
                BookIds = request.Books,
                Status = ReservationStatus.Pending
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            await _reservationProcessor.AddWorkAsync(request);
            return _mapper.Map<GetReservationSummaryResponseItem>(reservation);
        }

        public async Task<GetReservationSummaryResponse> GetAllReservationsAsync()
        {
            var response = new GetReservationSummaryResponse();
            var books = await _context.Reservations
                .ProjectTo<GetReservationSummaryResponseItem>(_config)
                .ToListAsync();


            response.Data = books;
            return response;
        }

        public async Task<GetReservationSummaryResponseItem> GetByIdAsync(int id)
        {
            var response = await _context.Reservations
                .Where(r => r.Id == id)
                .ProjectTo<GetReservationSummaryResponseItem>(_config)
                .SingleOrDefaultAsync();

            return response;
        }

        public async Task<bool> MarkDenied(GetReservationSummaryResponseItem item)
        {
            var reservation = await _context.Reservations.SingleOrDefaultAsync(r => r.Id == item.Id);
            if (reservation == null)
            {
                return false;
            }
            else
            {
                reservation.Status = ReservationStatus.Denied;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> MarkReady(GetReservationSummaryResponseItem item)
        {
            var reservation = await _context.Reservations.SingleOrDefaultAsync(r => r.Id == item.Id);
            if (reservation == null)
            {
                return false;
            }
            else
            {
                reservation.Status = ReservationStatus.Ready;
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
