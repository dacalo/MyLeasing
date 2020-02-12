namespace MyLeasing.Web.Helpers
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyLeasing.Web.Data;
    using System.Collections.Generic;
    using System.Linq;
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboPropertyTypes()
        {
            var list = _dataContext.PropertyTypes.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).OrderBy(p => p.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a property type...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboLessees()
        {
            var list = _dataContext.Lessees.Select(l => new SelectListItem
            {
                Text = l.User.FullNameWithRFC,
                Value = $"{l.Id}"
            })
                .OrderBy(pt => pt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a lessee...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "(Select a role...)" },
                new SelectListItem { Value = "1", Text = "Lessee" },
                new SelectListItem { Value = "2", Text = "Owner" }
            };

            return list;
        }
    }
}
