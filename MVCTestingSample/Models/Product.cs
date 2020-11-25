﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTestingSample.Models
{
    public class Product
    {
        [Key]
        public int ProdId { get; set; }
        [Required]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}
