﻿using Prism;
using Prism.Ioc;
using MyLeasing.Prism.ViewModels;
using MyLeasing.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyLeasing.Common.Services;
using MyLeasing.Common.Models;
using MyLeasing.Common.Helpers;
using Newtonsoft.Json;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyLeasing.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTU2MjMxQDMxMzcyZTMyMmUzMGg3Mnh0YUVXZHFHR2JUZVFybmlrSkVuNTFSMyt4RmJyNkNFemVFRXRoenc9");

            InitializeComponent();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
            {
                await NavigationService.NavigateAsync("/LeasingMasterDetailPage/NavigationPage/PropertiesPage");
            }
            else
            {
                await NavigationService.NavigateAsync("/NavigationPage/LoginPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<PropertiesPage, PropertiesPageViewModel>();
            containerRegistry.RegisterForNavigation<PropertyPage, PropertyPageViewModel>();
            containerRegistry.RegisterForNavigation<PropertyTabbedPage, PropertyTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<ContractsPage, ContractsPageViewModel>();
            containerRegistry.RegisterForNavigation<ContractPage, ContractPageViewModel>();
            containerRegistry.RegisterForNavigation<LeasingMasterDetailPage, LeasingMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
        }
    }
}
