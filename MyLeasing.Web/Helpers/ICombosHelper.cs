namespace MyLeasing.Web.Helpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPropertyTypes();

        IEnumerable<SelectListItem> GetComboLessees();

        IEnumerable<SelectListItem> GetComboRoles();
    }
}
