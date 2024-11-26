using BLL.Concrete;
using DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingCompanyWpf.Commands;
using TradingCompanyWpf.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TradingCompanyWpf.ViewModels
{
    public class SoldGoodsViewModel:BaseViewModel
    {
        private string connectionString;
        SoldGoodsDAL _sellDal;
        SoldGoodsServices _sellServices;
        public SoldGoodsViewModel(string conString)
        {
            connectionString = conString;
            _sellDal = new SoldGoodsDAL(connectionString);
            _sellServices = new SoldGoodsServices(_sellDal);
            _goods = new ObservableCollection<SoldGoodsModel>(LoadData());
            _filteredGoods = new ObservableCollection<SoldGoodsModel>(_goods);
            AddCommand = new RelayCommand(param => AddGoods());
        }

        private string _searchText;
        private ObservableCollection<SoldGoodsModel> _goods;
        private ObservableCollection<SoldGoodsModel> _filteredGoods;

        public ObservableCollection<SoldGoodsModel> FilteredGoods
        {
            get => _filteredGoods;
            set
            {
                _filteredGoods = value;
                OnPropertyChanged(nameof(FilteredGoods));
            }
        }
        public string TextSearch
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(TextSearch);
                    SearchCommand.Execute(null);
                }
            }
        }

        public RelayCommand SearchCommand => new RelayCommand(param => Search());
        public ICommand AddCommand { get; }
        private void Search()
        {
            if (string.IsNullOrEmpty(TextSearch))
            {
                FilteredGoods = new ObservableCollection<SoldGoodsModel>(_goods);
            }
            else
            {
                FilteredGoods = new ObservableCollection<SoldGoodsModel>(
                    _goods.Where(item => item.GoodsName.Contains(TextSearch, StringComparison.OrdinalIgnoreCase))
                );
            }
        }

        private IEnumerable<SoldGoodsModel> LoadData()
        {

            List<DTO.SoldGoods> sg = _sellServices.GetAll();
            List<SoldGoodsModel> sgm = new List<SoldGoodsModel>();
            foreach (var s in sg)
            {
                sgm.Add(SellMapper.MapToSellModel(s));
            }
            return sgm;
        }
        private void AddGoods()
        {
            var view = new AddSoldGoodsView(_sellServices);
            view.Show();
        }
    }
}
