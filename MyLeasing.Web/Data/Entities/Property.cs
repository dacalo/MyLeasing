namespace MyLeasing.Web.Data.Entities
{
    using MyLeasing.Common.Business;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Property
    {
        public int Id { get; set; }

        [Display(Name = "Estado*")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Neighborhood { get; set; }

        [Display(Name = "Domicilio*")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; }

        [Display(Name = "Precio*")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Metros cuadrados*")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SquareMeters { get; set; }

        [Display(Name = "Cuartos*")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Rooms { get; set; }

        [Display(Name = "Categoría*")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Stratum { get; set; }

        [Display(Name = "Tiene estacionamiento?")]
        public bool HasParkingLot { get; set; }

        [Display(Name = "Está disponible")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Observaciones")]
        public string Remarks { get; set; }

        [Display(Name = "Latitud")]
        [DisplayFormat(DataFormatString = "{0:N6}")]
        public double Latitude { get; set; }

        [Display(Name = "Longitud")]
        [DisplayFormat(DataFormatString = "{0:N6}")]
        public double Longitude { get; set; }


        public PropertyType PropertyType { get; set; }

        public Owner Owner { get; set; }

        public ICollection<PropertyImage> PropertyImages { get; set; }

        public ICollection<Contract> Contracts { get; set; }

        public string FirstImage => PropertyImages == null || PropertyImages.Count == 0
            ?  $"{Constants.URL_API}/images/Properties/noImage.png"
            : PropertyImages.FirstOrDefault().ImageUrl;
    }
}
