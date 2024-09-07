using Application.Features.DTOs;
using Application.Features.Interfaces.IServices;


namespace Application.Features.Queries.GeneralServices
{
    public  class PaginationHelper
    {
        public static PagedResponse<ICollection<T>> CreatePagedReponse<T>(ICollection<T> pagedData, PaginationFilter filter, IUriService uriService, string route)
        {
            var response = new PagedResponse<ICollection<T>>(pagedData, filter.PageNumber, filter.PageSize, filter.TotalRecords);
            var totalPages = ((double)filter.TotalRecords / (double)filter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            response.NextPage =
                filter.PageNumber >= 1 && filter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber + 1, filter.PageSize), route)
                : null;

            response.PreviousPage =
                filter.PageNumber - 1 >= 1 && filter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber - 1, filter.PageSize), route)
                : null;

            response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, filter.PageSize), route);
            response.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, filter.PageSize), route);
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = filter.TotalRecords;
            return response;

        }
    }
}
