﻿using MyLeasing.Common.Business;
using MyLeasing.Common.Helpers;
using MyLeasing.Common.Models;
using MyLeasing.Common.Services;
using MyLeasing.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Prism.ViewModels
{
    public class PropertiesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private OwnerResponse _owner;
        private ObservableCollection<PropertyItemViewModel> _properties;
        private bool _isRefreshing;
        private DelegateCommand _addPropertyCommand;
        private static PropertiesPageViewModel _instance;
        private DelegateCommand _refreshPropertiesCommand;

        public PropertiesPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.Properties;
            LoadProperties();
        }

        public ObservableCollection<PropertyItemViewModel> Properties
        {
            get => _properties;
            set => SetProperty(ref _properties, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public static PropertiesPageViewModel GetInstance()
        {
            return _instance;
        }

        public DelegateCommand AddPropertyCommand => _addPropertyCommand ?? (_addPropertyCommand = new DelegateCommand(AddPropertyAsync));

        public DelegateCommand RefreshPropertiesCommand => _refreshPropertiesCommand ?? (_refreshPropertiesCommand = new DelegateCommand(RefreshProperties));


        private void LoadProperties()
        {
            _owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);
            if (_owner.RoleId == 1)
            {
                Title = $"{Languages.PropertiesOf} {_owner.FullName}";
            }
            else
            {
                Title = Languages.AvailableProperties;
            }

            Properties = new ObservableCollection<PropertyItemViewModel>(_owner.Properties.Select(p => new PropertyItemViewModel(_navigationService)
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

        private async void AddPropertyAsync()
        {
            if(_owner.RoleId !=1)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ErrorNoOwner, Languages.Accept);
                return;
            }
            await _navigationService.NavigateAsync("EditPropertyPage");
        }

        public async Task UpdateOwnerAsync()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            if (!_apiService.CheckConnection())
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CheckConnection, Languages.Accept);
                return;
            }

            var response = await _apiService.GetOwnerByEmailAsync(
                Constants.URL_API,
                Constants.PREFIX,
                "Owners/GetOwnerByEmail",
                Constants.TokenType,
                token.Token,
                _owner.Email);

            if (response.IsSuccess)
            {
                var owner = (OwnerResponse)response.Result;
                Settings.Owner = JsonConvert.SerializeObject(owner);
                _owner = owner;
                LoadProperties();
            }
        }

        private async void RefreshProperties()
        {
            IsRefreshing = true;
            await UpdateOwnerAsync();
            IsRefreshing = false;
        }

    }
}
