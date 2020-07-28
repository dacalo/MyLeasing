using MyLeasing.Prism.Interfaces;
using MyLeasing.Prism.Resources;
using Xamarin.Forms;

namespace MyLeasing.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string EmailError => Resource.EmailError;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string Password => Resource.Password;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Rememberme => Resource.Rememberme;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string Login => Resource.Login;

        public static string Register => Resource.Register;

        public static string Loading => Resource.Loading;

        public static string ErrorNoOwner => Resource.ErrorNoOwner;

        public static string AddProperty => Resource.AddProperty;

        public static string Delete => Resource.Delete;

        public static string EditProperty => Resource.EditProperty;

        public static string ChangeImage => Resource.ChangeImage;

        public static string Neighborhood => Resource.Neighborhood;

        public static string NeighborhoodError => Resource.NeighborhoodError;

        public static string NeighborhoodPlaceHolder => Resource.NeighborhoodPlaceHolder;

        public static string Price => Resource.Price;

        public static string PriceError => Resource.PriceError;

        public static string PricePlaceHolder => Resource.PricePlaceHolder;

        public static string SquareMeters => Resource.SquareMeters;

        public static string SquareMetersError => Resource.SquareMetersError;

        public static string SquareMetersPlaceHolder => Resource.SquareMetersPlaceHolder;

        public static string Rooms => Resource.Rooms;

        public static string RoomsError => Resource.RoomsError;

        public static string RoomsPlaceHolder => Resource.RoomsPlaceHolder;

        public static string Stratum => Resource.Stratum;

        public static string StratumError => Resource.StratumError;

        public static string StratumPlaceHolder => Resource.StratumPlaceHolder;

        public static string PropertyType => Resource.PropertyType;

        public static string PropertyTypeError => Resource.PropertyTypeError;

        public static string PropertyTypePlaceHolder => Resource.PropertyTypePlaceHolder;

        public static string HasParkingLot => Resource.HasParkingLot;

        public static string IsAvailable => Resource.IsAvailable;

        public static string Remarks => Resource.Remarks;

        public static string Address => Resource.Address;

        public static string AddressError => Resource.AddressError;

        public static string AddressPlaceHolder => Resource.AddressPlaceHolder;

        public static string Save => Resource.Save;
        public static string PasswordError => Resource.PasswordError;
        public static string CheckConnection => Resource.CheckConnection;
        public static string ErrorLogin => Resource.ErrorLogin;
        public static string ErrorToken => Resource.ErrorToken;
        public static string NewProperty => Resource.NewProperty;
        public static string Details => Resource.Details;
        public static string Properties => Resource.Properties;
        public static string ChangePassword => Resource.ChangePassword;
        public static string Ok => Resource.Ok;
        public static string Contracts => Resource.Contracts;
        public static string AddImage => Resource.AddImage;
        public static string PictureSource => Resource.PictureSource;
        public static string Cancel => Resource.Cancel;
        public static string FromCamera => Resource.FromCamera;
        public static string FromGallery => Resource.FromGallery;
        public static string CreateEditPropertyConfirm => Resource.CreateEditPropertyConfirm;
        public static string Created => Resource.Created;
        public static string Edited => Resource.Edited;
        public static string Confirm => Resource.Confirm;
        public static string QuestionToDeleteProperty => Resource.QuestionToDeleteProperty;
        public static string Yes => Resource.Yes;
        public static string No => Resource.No;
        public static string EmailValidError => Resource.EmailValidError;
        public static string RecoverPassword => Resource.RecoverPassword;
        public static string ChangePasswordError => Resource.ChangePasswordError;
        public static string ChangePasswordLengthError => Resource.ChangePasswordLengthError;
        public static string ChangePasswordConfirm => Resource.ChangePasswordConfirm;
        public static string ChangePasswordConfirmError => Resource.ChangePasswordConfirmError;
        public static string Contract => Resource.Contract;
        public static string ContractTo => Resource.ContractTo;
        public static string ModifyUser => Resource.ModifyUser;
        public static string Map => Resource.Map;
        public static string Logout => Resource.Logout;
        public static string UserUpdate => Resource.UserUpdate;
        public static string DocumentError => Resource.DocumentError;
        public static string FirstNameError => Resource.FirstNameError;
        public static string LastNameError => Resource.LastNameError;
        public static string Property => Resource.Property;
        public static string NewUser => Resource.NewUser;
        public static string PhoneNumberError => Resource.PhoneNumberError;
        public static string RoleError => Resource.RoleError;
        public static string RFCPlaceHolder => Resource.RFCPlaceHolder;
        public static string FirstName => Resource.FirstName;
        public static string FirstNamePlaceHolder => Resource.FirstNamePlaceHolder;
        public static string LastName => Resource.LastName;
        public static string LastNamePlaceHolder => Resource.LastNamePlaceHolder;
        public static string Phone => Resource.Phone;
        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;
        public static string RegisterAs => Resource.RegisterAs;
        public static string RegisterAsTitle => Resource.RegisterAsTitle;
        public static string PasswordConfirm => Resource.PasswordConfirm;
        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;
        public static string Registering => Resource.Registering;
        public static string AddressPlaceHolder2 => Resource.AddressPlaceHolder2;
        public static string Lessee => Resource.Lessee;
        public static string Owner => Resource.Owner;
        public static string Saving => Resource.Saving;
        public static string Sending => Resource.Sending;
        public static string CurrentPassword => Resource.CurrentPassword;
        public static string CurrentPasswordPlaceHolder => Resource.CurrentPasswordPlaceHolder;
        public static string NewPassword => Resource.NewPassword;
        public static string NewPasswordPlaceHolder => Resource.NewPasswordPlaceHolder;
        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;
        public static string ConfirmNewPasswordPlaceHolder => Resource.ConfirmNewPasswordPlaceHolder;
        public static string Recovery => Resource.Recovery;
        public static string StartDate => Resource.StartDate;
        public static string EndDate => Resource.EndDate;
        public static string PropertiesOf => Resource.PropertiesOf;
        public static string AvailableProperties => Resource.AvailableProperties;
        public static string LoaderImage => Resource.LoaderImage;
        public static string Type => Resource.Type;
        public static string NotAddressFound => Resource.NotAddressFound;

        public static string NotLocationAvailable => Resource.NotLocationAvailable;

        public static string SelectAnAdrress => Resource.SelectAnAdrress;

    }
}
