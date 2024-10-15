using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Helper
{
    public class PaginatedResultDTO<T>
    {
        public PaginatedResultDTO(int pageindex,int pagesize,int count, IReadOnlyList<T> data) 
        { 
            PageIndex= pageindex;
            PageSize=pagesize;
            Count= count;
            Data= data;
        }

        public int PageIndex {  get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

            
    }
}
