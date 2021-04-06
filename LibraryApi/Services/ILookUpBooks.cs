using LibraryApi.Models.Books;
using System.Threading.Tasks;

namespace LibraryApi
{
    public interface ILookUpBooks
    {
        Task<GetBooksSummaryResponse> GetBooksByGenreAsync(string genre);
    }
}