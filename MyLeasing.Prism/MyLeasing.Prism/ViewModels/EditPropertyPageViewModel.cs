﻿using Acr.UserDialogs;
using MyLeasing.Common.Business;
using MyLeasing.Common.Helpers;
using MyLeasing.Common.Models;
using MyLeasing.Common.Services;
using MyLeasing.Prism.Helpers;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyLeasing.Prism.ViewModels
{
    public class EditPropertyPageViewModel : ViewModelBase
    {
        private PropertyResponse _property;
        private ImageSource _imageSource;
        private bool _isEnabled;
        private bool _isEdit;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ObservableCollection<PropertyTypeResponse> _propertyTypes;
        private PropertyTypeResponse _propertyType;
        private ObservableCollection<Stratum> _stratums;
        private Stratum _stratum;
        private MediaFile _file;
        private DelegateCommand _changeImageCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _deleteCommand;

        public EditPropertyPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
        }

        public bool IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public PropertyResponse Property
        {
            get => _property;
            set => SetProperty(ref _property, value);
        }

        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public ObservableCollection<PropertyTypeResponse> PropertyTypes
        {
            get => _propertyTypes;
            set => SetProperty(ref _propertyTypes, value);
        }

        public PropertyTypeResponse PropertyType
        {
            get => _propertyType;
            set => SetProperty(ref _propertyType, value);
        }

        public ObservableCollection<Stratum> Stratums
        {
            get => _stratums;
            set => SetProperty(ref _stratums, value);
        }

        public Stratum Stratum
        {
            get => _stratum;
            set => SetProperty(ref _stratum, value);
        }

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImage));

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        public DelegateCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(Delete));

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("property"))
            {
                Property = parameters.GetValue<PropertyResponse>("property");
                ImageSource = Property.FirstImage;
                IsEdit = true;
                Title = Languages.EditProperty;
            }
            else
            {
                Property = new PropertyResponse { IsAvailable = true };
                ImageSource = "noImage";
                IsEdit = false;
                Title = Languages.AddProperty;
            }

            LoadPropertyTypesAsync();
            LoadStratums();
        }

        private void LoadStratums()
        {
            Stratums = new ObservableCollection<Stratum>();
            for (int i = 1; i <= 6; i++)
            {
                Stratums.Add(new Stratum { Id = i, Name = $"{i}" });
            }

            Stratum = Stratums.FirstOrDefault(s => s.Id == Property.Stratum);
        }

        private async void LoadPropertyTypesAsync()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            if (!_apiService.CheckConnection())
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CheckConnection, Languages.Accept);
                return;
            }

            var response = await _apiService.GetListAsync<PropertyTypeResponse>(
                Constants.URL_API,
                Constants.PREFIX,
                "PropertyTypes",
                Constants.TokenType,
                token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var propertyTypes = (List<PropertyTypeResponse>)response.Result;
            PropertyTypes = new ObservableCollection<PropertyTypeResponse>(propertyTypes);

            if (!string.IsNullOrEmpty(Property.PropertyType))
            {
                PropertyType = PropertyTypes.FirstOrDefault(pt => pt.Name == Property.PropertyType);
            }
        }
               
        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.PictureSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.FromCamera);

            if (source == Languages.Cancel)
            {
                _file = null;
                return;
            }

            if (source == Languages.FromCamera)
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();
                    return stream;
                });
            }
        }

        private async void SaveAsync()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            UserDialogs.Instance.ShowLoading(Languages.Loading);
            IsEnabled = false;

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);

            var propertyRequest = new PropertyRequest
            {
                Address = Property.Address,
                HasParkingLot = Property.HasParkingLot,
                Id = Property.Id,
                IsAvailable = Property.IsAvailable,
                Neighborhood = Property.Neighborhood,
                OwnerId = owner.Id,
                Price = Property.Price,
                PropertyTypeId = PropertyType.Id,
                Remarks = Property.Remarks,
                Rooms = Property.Rooms,
                SquareMeters = Property.SquareMeters,
                Stratum = Stratum.Id
            };

            if (!_apiService.CheckConnection())
            {
                IsEnabled = true;
                UserDialogs.Instance.HideLoading();
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CheckConnection, Languages.Accept);
                return;
            }

            Response<object> response;
            if (IsEdit)
            {
                response = await _apiService.PutAsync(
                    Constants.URL_API,
                    Constants.PREFIX,
                    "Properties",
                    propertyRequest.Id,
                    propertyRequest,
                    Constants.TokenType,
                    token.Token);
            }
            else
            {
                response = await _apiService.PostAsync(
                    Constants.URL_API,
                    Constants.PREFIX,
                    "Properties",
                    propertyRequest,
                    Constants.TokenType,
                    token.Token);
            }

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = FilesHelper.ReadFully(_file.GetStream());
                if (Property.Id == 0)
                {
                    var response2 = await _apiService.GetLastPropertyByOwnerId(
                        Constants.URL_API,
                        Constants.PREFIX,
                        "Properties/GetLastPropertyByOwnerId",
                        Constants.TokenType,
                        token.Token,
                        owner.Id);
                    if (response2.IsSuccess)
                    {
                        var property = (PropertyResponse)response2.Result;
                        Property.Id = property.Id;
                    }
                }

                if (Property.Id != 0)
                {
                    var imageRequest = new ImageRequest
                    {
                        PropertyId = Property.Id,
                        ImageArray = imageArray
                    };

                    var response3 = await _apiService.PostAsync(
                        Constants.URL_API,
                        Constants.PREFIX,
                        "Properties/AddImageToProperty",
                        imageRequest,
                        Constants.TokenType,
                        token.Token);
                    if (!response3.IsSuccess)
                    {
                        UserDialogs.Instance.HideLoading();
                        IsEnabled = true;
                        await App.Current.MainPage.DisplayAlert(Languages.Error, response3.Message, Languages.Accept);
                    }
                }
            }

            if (!response.IsSuccess)
            {
                UserDialogs.Instance.HideLoading();
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await PropertiesPageViewModel.GetInstance().UpdateOwnerAsync();

            UserDialogs.Instance.HideLoading();
            IsEnabled = true;

            await App.Current.MainPage.DisplayAlert(
                Languages.Error,
                string.Format(Languages.CreateEditPropertyConfirm, IsEdit ? Languages.Edited : Languages.Created),
                Languages.Accept);

            await _navigationService.GoBackToRootAsync();
        }

        private async void Delete()
        {
            var answer = await App.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.QuestionToDeleteProperty,
                Languages.Yes,
                Languages.No);

            if (!answer)
            {
                return;
            }

            UserDialogs.Instance.ShowLoading(Languages.Loading);
            IsEnabled = false;

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            if (!_apiService.CheckConnection())
            {
                IsEnabled = true;
                UserDialogs.Instance.HideLoading();
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CheckConnection, Languages.Accept);
                return;
            }

            var response = await _apiService.DeleteAsync(
                Constants.URL_API,
                Constants.PREFIX,
                "Pets",
                Property.Id,
                Constants.TokenType,
                token.Token);

            if (!response.IsSuccess)
            {
                UserDialogs.Instance.HideLoading();
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await PropertiesPageViewModel.GetInstance().UpdateOwnerAsync();

            UserDialogs.Instance.HideLoading();
            IsEnabled = true;
            await _navigationService.GoBackToRootAsync();
        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(Property.Neighborhood))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.NeighborhoodError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Property.Address))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.AddressError, Languages.Accept);
                return false;
            }

            if (Property.Price <= 0)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PriceError, Languages.Accept);
                return false;
            }

            if (Property.SquareMeters <= 0)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.SquareMetersError, Languages.Accept);
                return false;
            }

            if (Property.Rooms <= 0)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.RoomsError, Languages.Accept);
                return false;
            }

            if (Stratum == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.StratumError, Languages.Accept);
                return false;
            }

            if (PropertyType == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PropertyTypeError, Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
