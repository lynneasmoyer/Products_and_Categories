using System.ComponentModel.DataAnnotations;

namespace ProdAndCat.Models
{
    public class Association
    {
        [Key]
        public int AssociationId {get; set;}
        public int ProductId {get; set;}
        public int CategoryId {get; set;}
        
        /////Navigational Properties

        public Product Product {get; set;}
        public Category Category {get; set;}
    }
}