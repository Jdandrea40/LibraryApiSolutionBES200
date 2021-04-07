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
        public EfReservationLookup(LibraryDataContext context, IMapper mapper, MapperConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        public async Task<GetReservationSummaryResponseItem> AddReservationAsync(PostReservationRequest request)
        {
            await Task.Delay(100 * request.Books.Split(',').Count());
            var reservation = new BookReservation
            {
                For = request.For,
                BookIds = request.Books,
                Status = ReservationStatus.Ready
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
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
    }
}
