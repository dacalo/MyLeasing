using Acr.UserDialogs;
using MyLeasing.Common.Business;
using MyLeasing.Common.Helpers;
using MyLeasing.Common.Models;
using MyLeasing.Common.Services;
using MyLeasing.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace MyLeasing.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private string _password;
        private bool _isRemember;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;
        private DelegateCommand _forgotPasswordCommand;

        public LoginPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            Title = Languages.Login;
            IsEnabled = true;
            IsRemember = true;

            //TODO: Delete those lines
            Email = "dacalo.soporte@gmail.com";
            Password = "123456";
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(Register));

        public DelegateCommand ForgotPasswordCommand => _forgotPasswordCommand ?? (_forgotPasswordCommand = new DelegateCommand(ForgotPassword));

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsRemember
        {
            get => _isRemember;
            set => SetProperty(ref _isRemember, value);
        }

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

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Password, Languages.Accept);
                return;
            }

            UserDialogs.Instance.ShowLoading(Languages.Loading);
            IsRunning = true;
            IsEnabled = false;

            if (!_apiService.CheckConnection())
            {
                IsEnabled = true;
                IsRunning = false;
                UserDialogs.Instance.HideLoading();
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.CheckConnection, Languages.Accept);
                return;
            }
            
            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var response = await _apiService.GetTokenAsync(Constants.URL_API, "Account", "/CreateToken", request);
            
            if (!response.IsSuccess)
            {
                IsRunning = false;
                UserDialogs.Instance.HideLoading();
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ErrorLogin, Languages.Accept);
                Password = string.Empty;
                return;
            }

            var token = response.Result;
            var response2 = await _apiService.GetOwnerByEmailAsync(Constants.URL_API, Constants.PREFIX, "Owners/GetOwnerByEmail", Constants.TokenType, token.Token, Email);

            if (!response2.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                UserDialogs.Instance.HideLoading();
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ErrorToken, Languages.Accept);
                return;
            }

            var owner = response2.Result;
            Settings.Owner = JsonConvert.SerializeObject(owner);
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.IsRemembered = IsRemember;
            await _navigationService.NavigateAsync("/LeasingMasterDetailPage/NavigationPage/PropertiesPage");
            IsRunning = false;
            UserDialogs.Instance.HideLoading();
            IsEnabled = true;
        }

        private async void Register()
        {
            await _navigationService.NavigateAsync("RegisterPage");

        }

        private async void ForgotPassword()
        {
            await _navigationService.NavigateAsync("RememberPasswordPage");
        }
    }
}
