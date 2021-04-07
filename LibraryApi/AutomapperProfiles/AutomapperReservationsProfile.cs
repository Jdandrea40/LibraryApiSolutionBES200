using AutoMapper;
using LibraryApi.Domain;
using LibraryApi.Models.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.AutomapperProfiles
{
    public class AutomapperReservationsProfile : Profile
    {
        public AutomapperReservationsProfile()
        {
            CreateMap<BookReservation, GetReservationSummaryResponseItem>();
        }
    }
}
