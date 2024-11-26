using AutoMapper;
using DTO;
using TradingCompanyApp.Models;

namespace TradingCompanyApp.App.Profiles
{
    public class SoldGoodsProfile:Profile
    {
        public SoldGoodsProfile()
        {
            CreateMap<SoldGoods, SoldGoodsDetailsModel>();
            CreateMap<SoldGoods, EditSoldGoodsModel>();
            CreateMap<EditSoldGoodsModel, SoldGoods>();
        }
    }
}
