using MyLeasing.Common.Helpers;
using MyLeasing.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLeasing.Prism.ViewModels
{
    public class PropertyTabbedPageViewModel : ViewModelBase
    {
        public PropertyTabbedPageViewModel(INavigationService navigationService) : base (navigationService)
        {
            var property = JsonConvert.DeserializeObject<PropertyResponse>(Settings.Property);
            Title = $"Property: {property.Neighborhood}";
        }
    }
}
