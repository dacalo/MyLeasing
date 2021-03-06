﻿using Acr.UserDialogs;
using MyLeasing.Common.Business;
using MyLeasing.Common.Helpers;
using MyLeasing.Common.Models;
using MyLeasing.Common.Services;
using MyLeasing.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;

namespace MyLeasing.Prism.ViewModels
{
    public class ModifyUserPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isEnabled;
        private OwnerResponse _owner;
        private DelegateCommand _saveCommand;
        private DelegateCommand _changePasswordCommand;

        public ModifyUserPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.ModifyUser;
            IsEnabled = true;
            Owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);
        }

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));
        public DelegateCommand ChangePasswordCommand => _changePasswordCommand ?? (_changePasswordCommand = new DelegateCommand(ChangePassword));


        public OwnerResponse Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void SaveAsync()
        {
            var isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            UserDialogs.Instance.ShowLoading(Languages.Saving);
            IsEnabled = false;

            var userRequest = new UserRequest
            {
                Address = Owner.Address,
                RFC = Owner.RFC,
                Email = Owner.Email,
                FirstName = Owner.FirstName,
                LastName = Owner.LastName,
                Password = "123456", //TODO It doesn't matter what is sent here. It is only for the model to be valid
                Phone = Owner.PhoneNumber
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            if (!_apiService.CheckConnection())
            {
                IsEnabled = true;
                UserDialogs.Instance.HideLoading();
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CheckConnection, Languages.Accept);
                return;
            }

            var response = await _apiService.PutAsync(
                Constants.URL_API,
                Constants.PREFIX,
                "Account",
                userRequest,
                Constants.TokenType,
                token.Token);

            UserDialogs.Instance.HideLoading();
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            Settings.Owner = JsonConvert.SerializeObject(Owner);

            await App.Current.MainPage.DisplayAlert(
                Languages.Ok,
                Languages.UserUpdate,
                Languages.Accept);

        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(Owner.RFC))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.DocumentError,
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Owner.FirstName))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.FirstNameError,
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Owner.LastName))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LastNameError,
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Owner.Address))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AddressError,
                    Languages.Accept);
                return false;
            }

            return true;
        }

        private async void ChangePassword()
        {
            await _navigationService.NavigateAsync("ChangePasswordPage");
        }

    }
}
