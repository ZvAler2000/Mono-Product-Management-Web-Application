using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Service.DTOs
{
    public class CreateCategoryDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
