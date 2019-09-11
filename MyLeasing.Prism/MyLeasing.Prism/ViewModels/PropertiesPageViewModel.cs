using System.Collections.ObjectModel;
using System.Linq;
using MyLeasing.Common.Models;
using Prism.Navigation;

namespace MyLeasing.Prism.ViewModels
{
    public class PropertiesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private OwnerResponse _owner;
        private TokenResponse _token;
        private ObservableCollection<PropertyResponse> _properties;
        private bool _isRefreshing;

        public PropertiesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Properties";
        }

        public ObservableCollection<PropertyResponse> Properties
        {
            get => _properties;
            set => SetProperty(ref _properties, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            IsRefreshing = true;
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("token"))
            {
                _token = parameters.GetValue<TokenResponse>("token");
            }

            if (parameters.ContainsKey("owner"))
            {
                _owner = parameters.GetValue<OwnerResponse>("owner");
                Properties = new ObservableCollection<PropertyResponse>(_owner.Properties.Select(p => new PropertyItemViewModel
                {
                    Address = p.Address,
                    Contracts = p.Contracts,
                    HasParkingLot = p.HasParkingLot,
                    Id = p.Id,
                    IsAvailable = p.IsAvailable,
                    Neighborhood = p.Neighborhood,
                    Price = p.Price,
                    PropertyImages = p.PropertyImages,
                    PropertyType = p.PropertyType,
                    Remarks = p.Remarks,
                    Rooms = p.Rooms,
                    SquareMeters = p.SquareMeters,
                    Stratum = p.Stratum
                }).ToList());
            }

            IsRefreshing = false;
        }
    }
}
