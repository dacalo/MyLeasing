using System.Threading.Tasks;
using Acr.UserDialogs;
using MyLeasing.Common.Business;
using MyLeasing.Common.Helpers;
using MyLeasing.Common.Models;
using MyLeasing.Common.Services;
using MyLeasing.Prism.Helpers;
using Prism.Commands;
using Prism.Navigation;

namespace MyLeasing.Prism.ViewModels
{
    public class RememberPasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isEnabled;
        private DelegateCommand _recoverCommand;

        public RememberPasswordPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.RecoverPassword;
            IsEnabled = true;
        }

        public DelegateCommand RecoverCommand => _recoverCommand ?? (_recoverCommand = new DelegateCommand(Recover));

        public string Email { get; set; }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void Recover()
        {
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            UserDialogs.Instance.ShowLoading(Languages.Recovery);
            IsEnabled = false;

            var request = new EmailRequest
            {
                Email = Email
            };

            if (!_apiService.CheckConnection())
            {
                IsEnabled = true;
                UserDialogs.Instance.HideLoading();
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CheckConnection, Languages.Accept);
                return;
            }

            var response = await _apiService.RecoverPasswordAsync(
                Constants.URL_API,
                Constants.PREFIX,
                "Account/RecoverPassword",
                request);

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

            await App.Current.MainPage.DisplayAlert(
                Languages.Ok,
                response.Message,
                Languages.Accept);
            await _navigationService.GoBackAsync();
        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(Email) || !RegexHelper.IsValidEmail(Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailValidError, Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
