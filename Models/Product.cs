using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProdAndCat.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}

        // [Range(0, double.MaxValue, ErrorMessage="Too much")]
        public double Price {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        ////Navigational property:
        public List <Association> AssocCategories {get; set;}
    }
}