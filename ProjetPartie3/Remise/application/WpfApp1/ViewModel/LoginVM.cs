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
        public event Action<string>? OnNotify;
        public event Action? OnLoginSuccess;
        [ObservableProperty]
        private string? _username, _password, _message;

        public LoginVM()
        {
            Message = "";
        }

        [RelayCommand]
        public async Task Login()
        {
            string? apikey;

            if (Username == null || Username == "" || Password == null || Password == "")
            {
                OnNotify?.Invoke(Ressources.Ressources.erreur_champsVides);
                return;
            }

            try
            {
                apikey = await ApiProcessor.Login(Username, Password);
            }
            catch (Exception ex)
            {
                //message Ressources
                OnNotify?.Invoke("ERR : " + ex.Message);
                return;
            }


            if (!string.IsNullOrEmpty(apikey))
            {
                ApiHelper.SetApiKeyHeader(apikey);
                OnNotify?.Invoke(Ressources.Ressources.msg_loginReussi);
                OnLoginSuccess?.Invoke();
                ResetValues();
            }
            else
            {
                OnNotify?.Invoke(Ressources.Ressources.erreur_loginFailed);
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
