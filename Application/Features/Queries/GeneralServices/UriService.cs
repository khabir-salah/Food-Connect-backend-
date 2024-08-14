using Application.Features.DTOs;
using Application.Features.Interfaces.IServices;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var modifiedUri = QueryHelpers.AddQueryString(endPointUri.ToString(), "PageNuber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", filter.PageNumber.ToString());
            return new Uri(modifiedUri);
        }
    }
}
