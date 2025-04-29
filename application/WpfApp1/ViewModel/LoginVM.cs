using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfApp1.ViewModel
{
    public partial class LoginVM : ObservableObject
    {
        [ObservableProperty]
        private string _username, _password, _message;

        public LoginVM()
        {
            Message = "";
        }

        [RelayCommand]
        public void Login()
        {
            if (string.IsNullOrEmpty(Username.Trim()) || string.IsNullOrEmpty(Password.Trim()))
            {
                Message = Ressources.Ressources.messageLoginInvalide
                return;
            }

            //todo
            //string result = await ApiProcessor.Login(Username, Password);
            //if (result == OKAY)
            ResetValues();

            //else Message = "Mot de passe ou nom d'utilisateur incorrecte.";

        }

        private void ResetValues()
        {
            Message = "";
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
