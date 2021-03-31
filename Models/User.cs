using System.ComponentModel.DataAnnotations;

namespace DataDriven.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Obrigatório")]
        [MaxLength(20, ErrorMessage="Máximo de 20 caracteres")]
        [MinLength(3, ErrorMessage="Mínimo de 3 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage="Obrigatório")]
        [MaxLength(20, ErrorMessage="Máximo de 20 caracteres")]
        [MinLength(3, ErrorMessage="Mínimo de 3 caracteres")]
        public string Password { get; set; }
        public string Role { get; set; }
        
    }
}