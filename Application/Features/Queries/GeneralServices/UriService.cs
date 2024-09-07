using Application.Features.DTOs;
using Application.Features.Interfaces.IServices;
using Microsoft.AspNetCore.WebUtilities;


namespace Application.Features.Queries.GeneralServices
{
    public class UriService : IUriService
    {
        private readonly string _uri;
        public UriService(string uri)
        {
            _uri = uri;
        }

        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var endPointUri = new Uri(string.Concat(_uri, route));

            var modifiedUri = QueryHelpers.AddQueryString(endPointUri.ToString(), "PageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", filter.PageSize.ToString());  

            return new Uri(modifiedUri);
        }

    }
}
