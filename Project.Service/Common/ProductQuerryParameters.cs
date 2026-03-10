using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Project.Service.Common
{
    public class ProductQuerryParameters
    {
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsActive { get; set; }
        public string? SortBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
