using System.ComponentModel.DataAnnotations;

namespace DataDriven.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        //Campo necessário, min de 3 e máx de 60
        [Required(ErrorMessage = "O campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        [MinLength(3, ErrorMessage = "Mínimo de 3 caracteres")]
        public int Title { get; set; }        

        [MaxLength(1024, ErrorMessage = "Máximo de 1024 caracteres")]
        public int Description { get; set; }

        //Obrigatório, sendo sempre > 1
        [Required(ErrorMessage = "O campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        //possibilidade d e incluir a query de categoria
        public Category Category { get; set; }

        
    }
}