using System.ComponentModel.DataAnnotations;

namespace DataDriven.Models
{
    public class Category
    {
       
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        [MinLength(3, ErrorMessage = "Mínimo de 3 caracteres")]
        public string Title { get; set; }
    }
}