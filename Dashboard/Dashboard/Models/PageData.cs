using cloudscribe.Pagination.Models;
using Dashboard.Interfaces;

namespace Dashboard.Models
{
    public class PageData
    {
        public static PagedResult<T> GetPage<T>(List<T> viewModels, int totalItems, int pageNumber, int pageSize) where T : class, IBaseEntity
        {
            return new PagedResult<T>
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = viewModels
            };
        }
    }
}
