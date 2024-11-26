using BLL.Concrete;
using DAL.Concrete;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TradingCompanyWpf.Commands;

namespace TradingCompanyWpf.ViewModels
{
    public class ProgressDiagramViewModel:BaseViewModel
    {
        private string _connectionString;
        private SoldGoodsServices _soldGoodsServices;
        private OrderedGoodsServices _orderedGoodsServices;
        public ProgressDiagramViewModel(string connectionString)
        {
            _connectionString = connectionString;
            SoldGoodsDAL sg = new SoldGoodsDAL(connectionString);
            OrderGoodsDAL og = new OrderGoodsDAL(connectionString);
            _soldGoodsServices = new SoldGoodsServices(sg);
            _orderedGoodsServices = new OrderedGoodsServices(og);
            StartDate = new DateTime(DateTime.Today.Year, 1, 1);
            EndDate = DateTime.Today;
            PrintSumProfit();

        }

        private DateTime _startTime;
        private DateTime _endTime;
        public DateTime StartDate
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartDate));
                PrintSumProfit();
            }
        }
        public DateTime EndDate
        {
            get => _endTime;
            set
            {
                if (_startTime <= _endTime || _endTime == new DateTime(1, 1, 1))
                {
                    _endTime = value;
                    OnPropertyChanged(nameof(EndDate));
                    PrintSumProfit();
                }
                else
                {
                    MessageBox.Show("End Date must be after Start Date!");
                }
            }
        }

        private string _sumProfit;
        public string SumProfit
        {
            get => _sumProfit;
            set
            {
                _sumProfit = value;
                OnPropertyChanged(nameof(SumProfit));
            }
        }
        private void PrintSumProfit()
        {
                List<OrderedGoods> og = _orderedGoodsServices.GetAll().Where(sold =>
                        sold.Date >= StartDate &&
                        sold.Date <= EndDate).ToList();
                List<SoldGoods> sg = _soldGoodsServices.GetAll().Where(sold =>
                        sold.Date >= StartDate &&
                        sold.Date <= EndDate).ToList();
            double resOrder = 0;
                double resSold = 0;
                foreach (var o in og)
                {
                    resOrder += (double)o.Cost_Lost * o.Amount_Get;
                }
                foreach (var s in sg)
                {
                    resSold += (double)s.Cost_Get * s.Amount_Lost;
                }
                SumProfit = $"Money spent: {resOrder}\nMoney received: {resSold}\nGeneral profit: {resSold - resOrder}";

        }
    }
}
