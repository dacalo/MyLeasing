﻿using System.Threading.Tasks;
using MyLeasing.Common.Helpers;
using MyLeasing.Common.Models;
using MyLeasing.Common.Services;
using MyLeasing.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace MyLeasing.Prism.ViewModels
{
    public class ChangePasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _changePasswordCommand;

        public ChangePasswordPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
            Title = Languages.ChangePassword;
        }

        public DelegateCommand ChangePasswordCommand => _changePasswordCommand ?? (_changePasswordCommand = new DelegateCommand(ChangePasswordAsync));

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string PasswordConfirm { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void ChangePasswordAsync()
        {
            var isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var request = new ChangePasswordRequest
            {
                Email = owner.Email,
                NewPassword = NewPassword,
                OldPassword = CurrentPassword
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.ChangePasswordAsync(
                url,
                "/api",
                "/Account/ChangePassword",
                request,
                "bearer",
                token.Token);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(
                Languages.Ok,
                response.Message,
                Languages.Accept);

            await _navigationService.GoBackAsync();

        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter your current password.",
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(NewPassword) || NewPassword?.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter a new password at least 6 characters lenth.",
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "You must enter your a password confirm.",
                    Languages.Accept);
                return false;
            }

            if (!NewPassword.Equals(PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "The password and confirmation does not match.",
                    Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
