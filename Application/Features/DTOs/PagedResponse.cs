using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DTOs
{
    public class PagedResponse<T> : BaseResponse<T>
    {
        public int PageNumber { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public Uri LastPage { get; set; }
        public Uri FirstPage { get; set; }


        public PagedResponse(T data, int pageSize, int pageNumber, int totalRecords)
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.Data = data;
            this.Message = string.Empty;
            this.IsSuccessfull = true;
            this.TotalRecords = totalRecords;
        }
            
    }


    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 2;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 2 ? 2 : pageSize;
        }
    }

   
}
