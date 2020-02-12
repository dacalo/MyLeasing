namespace MyLeasing.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class PropertyImage
    {
        public int Id { get; set; }

        [Display(Name = "Foto")]
        public string ImageUrl { get; set; }

        public Property Property { get; set; }

        // TODO: Change the path when publish
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl) ? null : $"https://TBD.azurewebsites.net{ImageUrl.Substring(1)}";

    }
}
