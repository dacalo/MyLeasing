namespace MyLeasing.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Display(Name = "RFC")]
        [StringLength(13, MinimumLength = 12, ErrorMessage = "")]
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

        [Display(Name = "Nombre Completo")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Nombre Completo con RFC")]
        public string FullNameWithRFC => $"{FirstName} {LastName} - {RFC}";
    }
}
