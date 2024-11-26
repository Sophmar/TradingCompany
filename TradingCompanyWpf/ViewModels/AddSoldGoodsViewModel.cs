using BLL.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TradingCompanyWpf.Commands;
using TradingCompanyWpf.Models;

namespace TradingCompanyWpf.ViewModels
{
    public class AddSoldGoodsViewModel: BaseViewModel, IDataErrorInfo
    {
        private SoldGoodsServices _sellServices;
        public AddSoldGoodsViewModel(SoldGoodsServices sellServices) 
        {
            _sellServices = sellServices;
            AddCommand = new RelayCommand(Add, param => string.IsNullOrEmpty(Error));
        }
        private string _productName;
        private string _productCost;
        private string _productAmount;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }
        public string ProductCost
        {
            get => _productCost;
            set
            {
                _productCost = value;
                OnPropertyChanged(nameof(ProductCost));
            }
        }
        public string ProductAmount
        {
            get => _productAmount;
            set
            {
                _productAmount = value;
                OnPropertyChanged(nameof(ProductAmount));
            }
        }

        public ICommand AddCommand { get; }

        public void Add(object parameter)
        {
            SoldGoodsModel model = new SoldGoodsModel()
            {
                Id = 0,
                GoodsName = ProductName,
                Cost = Convert.ToDecimal(ProductCost),
                Amount = Convert.ToInt32(ProductAmount),
                Date = DateTime.Now
            };
            int res = _sellServices.Add(SellMapper.MapToSell(model));
            if (res == 0)
                MessageBox.Show("Was not added!");
            else
            {
                MessageBox.Show("Added successfully!");
                ProductName = "";
                ProductCost = "";
                ProductAmount = "";
            }

        }

        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (columnName == "ProductName")
                {
                    result = ValidateName();
                }
                if (columnName == "ProductCost")
                {
                    result = ValidateCost();
                }
                if (columnName == "ProductAmount")
                {
                    result = ValidateAmount();
                }
                return result;
            }
        }

        public string Error
        {
            get
            {
                return ValidateName() + ValidateCost() + ValidateAmount();
            }
        }

        private string ValidateName()
        {
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                return "Product`s name cannot be empty";
            }
            return string.Empty;
        }

        private string ValidateCost()
        {
            if (string.IsNullOrWhiteSpace(ProductCost))
            {
                return "Product`s cost cannot be empty";
            }
            else if (Regex.IsMatch(ProductCost, @"[^0-9.,]"))
            {
                return "Only 1234567890., are allowed";
            }
            else if (ProductCost == "0")
            {
                return "Cost cannot be 0";
            }
            return string.Empty;
        }

        private string ValidateAmount()
        {
            if (string.IsNullOrWhiteSpace(ProductAmount))
            {
                return "Product`s amount cannot be empty";
            }
            else if (Regex.IsMatch(ProductCost, @"[^0-9]"))
            {
                return "Only 1234567890 are allowed";
            }
            else if (ProductAmount == "0")
            {
                return "Amount cannot be 0";
            }
            return string.Empty;
        }

    }
}
