﻿using BLL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TradingCompanyWpf.ViewModels;

namespace TradingCompanyWpf
{
    /// <summary>
    /// Interaction logic for AddSoldGoodsView.xaml
    /// </summary>
    public partial class AddSoldGoodsView : Window
    {
        public AddSoldGoodsView(SoldGoodsServices soldServices)
        {
            InitializeComponent();
            this.DataContext = new AddSoldGoodsViewModel(soldServices);
        }
    }
}