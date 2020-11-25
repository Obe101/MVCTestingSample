using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTestingSample.Models
{
    public class Product
    {
        private string name;

        [Key]
        public int ProdId { get; set; }


     
        [Required]
        public string Name
        {
            get => name;
            set
            {
                if (value == null)
                
                    throw new ArgumentNullException($"{nameof(name)} cannot be null");
                    
                    name = value;
                

            }
        }
        public double Price { get; set; }
    }
}
