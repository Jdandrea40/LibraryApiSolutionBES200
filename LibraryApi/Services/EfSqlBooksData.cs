using AutoMapper;
using LibraryApi.Domain;
using LibraryApi.Models.Books;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Services
{
    public class EfSqlBooksData : ILookUpBooks
    {
        private readonly LibraryDataContext _context;
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        public EfSqlBooksData(LibraryDataContext context, MapperConfiguration config, IMapper mapper)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
        }

        public async Task<GetBooksSummaryResponse> GetBooksByGenreAsync(string genre)
        {

            var query = _context.AvailableBooks;
            if (genre != null)
            {
                query = query.Where(b => b.Genre == genre);
            };

            var data = await query.ProjectTo<BookSummaryItem>(_config).ToListAsync();

            var response = new GetBooksSummaryResponse
            {
                Data = data,
                GenreFilter = genre
            };

            return response;
        }
    }
}
