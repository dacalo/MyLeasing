using MyLeasing.Common.Helpers;
using MyLeasing.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace MyLeasing.Prism.ViewModels
{
    public class PropertyItemViewModel : PropertyResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectPropertyCommand;

        public PropertyItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectPropertyCommand => _selectPropertyCommand ?? (_selectPropertyCommand = new DelegateCommand(SelectProperty));

        private async void SelectProperty()
        {
            //TODO Eliminar Comentarios
            //var parameters = new NavigationParameters
            //{
            //    { "property", this }
            //};

            ////await _navigationService.NavigateAsync("PropertyPage", parameters);
            //await _navigationService.NavigateAsync("ContractsPage", parameters);
            Settings.Property = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync("PropertyTabbedPage");
        }
    }
}