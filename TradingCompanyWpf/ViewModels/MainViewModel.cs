using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TradingCompanyWpf.Commands;
using TradingCompanyWpf.Models;

namespace TradingCompanyWpf.ViewModels
{
    class MainViewModel: BaseViewModel
    {
        UserModel _user;
        string connectionString;
        public MainViewModel(string conString, UserModel user) 
        {
            connectionString = conString;
            _user = new UserModel()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Salt = user.Salt,
                AccessId = user.AccessId
            };
            if (_user.AccessId == 0)
            {
                IsBtnEnabled = false;
            }
            else
                IsBtnEnabled = true;
            CurrentView = new Homepage();
            
        }
        private bool _isEnabled;

        private object _currentView;
        public bool IsBtnEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsBtnEnabled));
            }
        }
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowHomepageCommand => new RelayCommand(p => ShowHomepage());
        public ICommand ShowSoldGoodsCommand => new RelayCommand(p => ShowSoldGoods());
        public ICommand ShowOrderedGoodsCommand => new RelayCommand(p => ShowOrderedGoods());
        public ICommand ShowDiagramCommand => new RelayCommand(p => ShowDiagram());
        public ICommand LogOutCommand => new RelayCommand(p => LogOut());

        public void ShowHomepage()
        {
            CurrentView = new Homepage();
        }
        public void ShowSoldGoods()
        {
            CurrentView = new SoldGoodsView(connectionString);
        }
        public void ShowOrderedGoods()
        {
            CurrentView = new OrderedGoodsView(connectionString);
        }
        public void ShowDiagram()
        {
            CurrentView = new ProgressDiagramView(connectionString);
        }
        public void LogOut()
        {
            LoginWindow loginWindow = new LoginWindow();
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
            loginWindow.Show();
        }
    }
}
