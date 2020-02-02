using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProdAndCat.Models
{
    public class Category
    {
        [Key]
        public int CategoryId {get; set;}
        public string Name {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        ////Navigational Property

        public List<Association> AssocProducts {get; set;}
        
    }
}