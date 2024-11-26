using BLL.Concrete;
using DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingCompanyWpf.Commands;
using TradingCompanyWpf.Models;
using DTO;
using System.CodeDom;

namespace TradingCompanyWpf.ViewModels
{
    public class OrderedGoodsViewModel:BaseViewModel
    {
        private string connectionString;
        OrderGoodsDAL _orderDal;
        OrderedGoodsServices _orderServices;
        public OrderedGoodsViewModel(string conString)
        {
            connectionString = conString;
            _orderDal = new OrderGoodsDAL(connectionString);
            _orderServices = new OrderedGoodsServices(_orderDal);
            _goods = new ObservableCollection<OrderedGoodsModel>(LoadData());
            _filteredGoods = new ObservableCollection<OrderedGoodsModel>(_goods);
        }

        private string _searchText;
        private ObservableCollection<OrderedGoodsModel> _goods;
        private ObservableCollection<OrderedGoodsModel> _filteredGoods;

        public ObservableCollection<OrderedGoodsModel> FilteredGoods
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
        private void Search()
        {
            if (string.IsNullOrEmpty(TextSearch))
            {
                FilteredGoods = new ObservableCollection<OrderedGoodsModel>(_goods);
            }
            else
            {
                FilteredGoods = new ObservableCollection<OrderedGoodsModel>(
                    _goods.Where(item => item.GoodsName.Contains(TextSearch, StringComparison.OrdinalIgnoreCase))
                );
            }
        }
        
        private IEnumerable<OrderedGoodsModel> LoadData()
        {

            List<DTO.OrderedGoods> og = _orderServices.GetAll();
            List<OrderedGoodsModel> ogm = new List<OrderedGoodsModel>();
            foreach (var o in og)
            {
                ogm.Add(OrderMapper.MapToOrderModel(o));
            }
            return ogm;
        }
    }
}
