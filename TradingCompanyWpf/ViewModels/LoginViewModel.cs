using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TradingCompanyWpf.Commands;

namespace TradingCompanyWpf.ViewModels
{
    public class LoginViewModel: INotifyPropertyChanged, IDataErrorInfo
    {

        public LoginViewModel() 
        {
            LoginCommand = new RelayCommand(Login, param => string.IsNullOrEmpty(Error));
        }

        public ICommand LoginCommand { get; }
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _login = string.Empty;
        private string _password = string.Empty;

        public string UserLogin
        {
            get => _login;
            set
            {
                if (_login != value)
                {
                    _login = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserLogin)));
                }
            }
        }

        public string PasswordUserInput
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PasswordUserInput)));
                }
            }
        }

        public ICommand GreetCommand { get; }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (columnName == "UserNameInput")
                {
                    result = ValidateLogin();
                }
                if(columnName == "PasswordUserInput")
                {
                    result = ValidatePassword();
                }
                return result;
            }
        }

        public string Error
        {
            get
            {
                return ValidateLogin() + ValidatePassword();
            }
        }

        private string ValidateLogin()
        {
            if (string.IsNullOrWhiteSpace(UserLogin))
            {
                return "Login cannot be empty";
            }
            else if (UserLogin.Contains(" "))
            {
                return "Using space is not allowed";
            }
            return string.Empty;
        }

        private string ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(PasswordUserInput))
            {
                return "Password cannot be empty";
            }
            else if (PasswordUserInput.Contains(" "))
            {
                return "Using space is not allowed";
            }
            return string.Empty;
        }

    }
}
