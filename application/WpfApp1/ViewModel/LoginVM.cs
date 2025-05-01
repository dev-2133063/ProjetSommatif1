using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp1.Ressources;

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
        public async void Login()
        {
            string? apikey;

            if (string.IsNullOrEmpty(Username.Trim()) || string.IsNullOrEmpty(Password.Trim()))
            {
                //todo bon message
                //Message = Ressources.Ressources.messageRemplirChamps;
                return;
            }

            try
            {
                apikey = await ApiProcessor.Login(Username, Password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (apikey != null /*|| result.Status = Satus.Ok*/)
            {
                ConfigurationManager.AppSettings["apikey"] = apikey;

                ResetValues();
            }
            else
            {
                //Message = Ressources.Ressources.MessageLoginInvalide;
            }


        }

        private void ResetValues()
        {
            Message = "";
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
