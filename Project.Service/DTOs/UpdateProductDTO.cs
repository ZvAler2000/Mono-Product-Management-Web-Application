using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Service.DTOs
{
    public class UpdateProductDTO
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
    }
}
