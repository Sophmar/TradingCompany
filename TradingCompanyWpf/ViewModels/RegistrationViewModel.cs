using BLL.Concrete;
using DAL.Concrete;
using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using TradingCompanyWpf.Commands;
using TradingCompanyWpf.Models;

namespace TradingCompanyWpf.ViewModels
{
    public class RegistrationViewModel:BaseViewModel, IDataErrorInfo
    {
        UserDAL _userDal;
        UserServices _userServices;
        public RegistrationViewModel(string connectionString)
        {
            _userDal = new UserDAL(connectionString);
            _userServices = new UserServices(_userDal);
            RegisterCommand = new RelayCommand(Register, param => string.IsNullOrEmpty(Error));
        }

        private string _login = string.Empty;
        private string _password = string.Empty;
        private string _confirmPassword = string.Empty;
        //private string _errorMessage = string.Empty;
        //public string ErrorMessage
        //{
        //    get => Error;
        //    set
        //    {
        //        if (_errorMessage != value)
        //        {
        //            _errorMessage = value;
        //            OnPropertyChanged(Error);
        //        }
        //    }
        //}
        public string Login
        {
            get => _login;
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged(nameof(ConfirmPassword));
                }
            }
        }
        public ICommand RegisterCommand { get; }
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (columnName == "Login")
                {
                    result = ValidateLogin();
                }
                if (columnName == "Password")
                {
                    result = ValidatePassword();
                }
                if (columnName == "ConfirmPassword")
                {
                    result = ValidateConfirmPassword();
                }
                return result;
            }
        }

        public string Error
        {
            get
            {
                return ValidateLogin() + ValidatePassword() + ValidateConfirmPassword();
            }
        }

        private string ValidateLogin()
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                return "Login cannot be empty";
            }
            else if (Login.Contains(" "))
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

        private string ValidateConfirmPassword()
        {
            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                return "Confirm Password cannot be empty";
            }
            else if (ConfirmPassword.Contains(" "))
            {
                return "Using space is not allowed";
            }
            else if (ConfirmPassword != Password)
            {
                return "Passwords in Password and Confirm Password differs!";
            }
            return string.Empty;
        }
        public void Register(object parameter)
        {
            User u = new User()
            {
                UserId = 0,
                Login = Login
            };

            var res = _userServices.Add(u, Password);
            if(res != 0)
            {
                MessageBox.Show("Successful registration!");
                Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
            }
            else
            {
                MessageBox.Show("Invalid input. Please try again.");
            }
        }
    }
}
