using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TradingCompanyWpf.Models;
using TradingCompanyWpf.ViewModels;

namespace TradingCompanyWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string connectionString, UserModel user)
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(connectionString, user);
        }

    }
}