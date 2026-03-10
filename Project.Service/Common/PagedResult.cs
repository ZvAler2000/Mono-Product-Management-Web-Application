using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Service.Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items {  get; set; }
        public int Count { get; set; }

        public PagedResult(IEnumerable<T> items, int count)
        {
            Items = items;
            Count = count;
        }
    }
}
