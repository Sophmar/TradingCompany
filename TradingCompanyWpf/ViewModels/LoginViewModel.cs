using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TradingCompanyWpf.Commands;
using DAL.Concrete;
using BLL.Concrete;
using BLL.Interfaces;
using TradingCompanyWpf.Models;
using DTO;

namespace TradingCompanyWpf.ViewModels
{
    public class LoginViewModel : BaseViewModel, IDataErrorInfo
    {
        private string connectionString;
        private string _userName = string.Empty;
        public string Username
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(Username);
                }
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(Password);
                }
            }
        }
        public ICommand LogInCommand { get; }
        public ICommand RegisterCommand { get; }

        UserDAL udal;
        UserServices userv;

        public LoginViewModel(string conString)
        {
            connectionString = conString;
            udal = new UserDAL(conString);
            userv = new UserServices(udal);
            LogInCommand = new RelayCommand(Login, param => string.IsNullOrEmpty(Error));
            RegisterCommand = new RelayCommand(p => Register());
        }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (columnName == "UserNameInput")
                {
                    result = ValidateLogin();
                }
                if (columnName == "PasswordUserInput")
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
            if (string.IsNullOrWhiteSpace(Username))
            {
                return "Login cannot be empty";
            }
            else if (Username.Contains(" "))
            {
                return "Using space is not allowed";
            }
            return string.Empty;
        }

        private string ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                return "Password cannot be empty";
            }
            else if (Password.Contains(" "))
            {
                return "Using space is not allowed";
            }
            return string.Empty;
        }
        private void Login(object parameter)
        {
            int isAuthenticated = userv.Authentication(Username, Password);

            if (isAuthenticated >= 0)
            {
                UserModel model = UserMapper.MapToUserModel(userv.GetUserByLogin(Username));
                model.AccessId = isAuthenticated;
                MainWindow mainWindow = new MainWindow(connectionString, model);
                Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
        private void Register()
        {
            RegistrationWindow rg = new RegistrationWindow(connectionString);
            rg.Show();
        }
    }
}
