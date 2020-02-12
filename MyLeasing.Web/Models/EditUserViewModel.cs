using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Models
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "RFC")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string RFC { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Domicilio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres.")]
        public string Address { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres.")]
        public string PhoneNumber { get; set; }
    }
}
