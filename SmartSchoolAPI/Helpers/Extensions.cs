using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SmartSchoolAPI.Helpers
{
    public static class Extensions
    {
        public static void AddPagination( this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages) 
        {
            var PaginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(PaginationHeader));
        }
    }
}