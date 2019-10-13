﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using MyLeasing.Common.Helpers;
using MyLeasing.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;

namespace MyLeasing.Prism.ViewModels
{
    public class PropertiesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private OwnerResponse _owner;
        private TokenResponse _token;
        private ObservableCollection<PropertyItemViewModel> _properties;
        private bool _isRefreshing;

        public PropertiesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadOwner();
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

        private void LoadOwner()
        {
            _owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);
            if (_owner.RoleId == 1)
            {
                Title = $"Properties of: {_owner.FullName}";
            }
            else
            {
                Title = "Available Properties";
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


    }
}